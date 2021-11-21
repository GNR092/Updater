using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Updater.cfg;

namespace Updater
{
    public partial class Updater : Form
    {
        readonly string url = "https://streaminvick.duckdns.org/nextcloud/s/MDCHpqjpmAT9j5r/download?path=%2F&files=";
        private readonly string Data = "Data.dat";
        private readonly string OldData = "Data_old.dat";
        private WebClient Client;
        private bool downloadComplete = false;
        private string file = null, md5 = null, dir = null;
        private int Maxitems = 0;
        private string[] strArray;
        private int conteo;
        private readonly Config cfg;

        public Updater()
        {
            try
            {
                InitializeComponent();
                if (File.Exists("_Updater.temp"))
                {
                    File.Delete("_Updater.temp");
                }
                cfg = Config.Load("Config.dat");
                if (!Directory.Exists(cfg.MinecraftDirectory + "\\mods"))
                {
                    Directory.CreateDirectory(cfg.MinecraftDirectory + "\\mods");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            try
            {
                await Task.Run(() => CheckGameData(Data));
                await Task.Run(() => DownloadFile(url + Data, Data));
                await Task.Run(() => Gamedatas());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void DownloadFile(string direccionurl, string Archivo)
        {
            using (Client = new WebClient())
            {
                try
                {
                    Client.DownloadFileCompleted += Completado;
                    Client.DownloadProgressChanged += EstadoDelProgreso;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3;


                    Uri direccion = new Uri(direccionurl);
                    SetLabel1TextSafe("Descargando: " + Path.GetFileName(Archivo));
                    Client.DownloadFileAsync(direccion, Archivo);
                    while (!downloadComplete)
                    {
                        Application.DoEvents();
                    }
                    downloadComplete = false;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void SetLabel1TextSafe(string txt)
        {
            lb_name.Invoke(new Action(() => lb_name.Text = txt));
        }
        private void SetProgressSafe(int p)
        {
            pb_status.Invoke(new Action(() => pb_status.Value = p));
        }

        private void EstadoDelProgreso(object sender, DownloadProgressChangedEventArgs e)
        {
            SetProgressSafe(e.ProgressPercentage);
            SetLabelPercentTextSafe(e.ProgressPercentage);
        }

        private void SetLabelPercentTextSafe(int progressPercentage)
        {
            lb_percent.Invoke(new Action(() => lb_percent.Text = $"{progressPercentage}%"));
        }

        private void Completado(object sender, AsyncCompletedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate { downloadComplete = true; });
        }

        private async void Gamedatas()
        {
            try
            {
                string Gdata1, Gdata2;
                if (File.Exists(OldData))
                {
                    string lineas2;
                    StreamReader _data = new StreamReader(Data);
                    string[] _lineas2;
                    while ((lineas2 = _data.ReadLine()) != null)
                    {
                        _lineas2 = lineas2.Split(char.Parse(":"));
                        if (string.IsNullOrEmpty(_lineas2[0]))
                        {
                            break;
                        }
                        Maxitems++;
                    }
                    Gdata1 = await Task.Run(() => GetHashMD5(Data));
                    Gdata2 = await Task.Run(() => GetHashMD5(OldData));

                    if (string.Compare(Gdata1, Gdata2, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        StreamReader _file = new StreamReader(Data);
                        string str;

                        while ((str = _file.ReadLine()) != null)
                        {
                            strArray = str.Split(char.Parse(":"));
                            file = strArray[0];
                            md5 = strArray[1];
                            var _urlC = url + file;

                            if (string.IsNullOrEmpty(strArray[0]))
                            {
                                break;
                            }
                            if (file == "_Updater.exe")
                            {
                                string md5str = await Task.Run(() => GetHashMD5(file));
                                if (!md5.Equals(md5str))
                                {
                                    if (File.Exists(file))
                                    {
                                        File.Move(file, file.Replace(".exe", ".temp"));
                                        await Task.Run(() => DownloadFile(_urlC, file));
                                    }
                                    System.Diagnostics.Process.Start("_Updater.exe");
                                    Application.Exit();
                                    break;
                                }
                            }
                            else
                            {
                                if (!File.Exists($"{cfg.MinecraftDirectory}\\mods\\{file}"))
                                {
                                    conteo++;
                                    SetProgressSafe((conteo * 100) / Maxitems);

                                    await Task.Run(() => DownloadFile(_urlC, $"{cfg.MinecraftDirectory}\\mods\\{file}"));

                                }
                                else
                                {

                                    await Task.Run(() => Comprobar());

                                }
                            }
                        }

                        SetLabel1TextSafe("Actualizado");
                        SetSafeButton(btn_exit, true);


                    }
                }
                else
                {
                    string lineas;
                    StreamReader _file = new StreamReader(Data);
                    while ((lineas = _file.ReadLine()) != null)
                    {
                        Maxitems++;
                    }
                    #region Crear Listado de Arrays y sus Comprobaciones
                    DescargarArchivosGamedata();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetSafeButton(Button btn, bool v)
        {
            btn.Invoke(new Action(() => btn.Enabled = v));
        }

        private async void DescargarArchivosGamedata()
        {
            try
            {
                StreamReader _file = new StreamReader(Data);
                string str;
                string _urlC;
                string nuevofile;

                while ((str = _file.ReadLine()) != null)
                {
                    strArray = str.Split(char.Parse(":"));
                    file = strArray[0];
                    md5 = strArray[1];
                    _urlC = url + file;
                    if (file == "_Updater.exe")
                    {
                        string md5str = await Task.Run(() => GetHashMD5(file));
                        if (!md5.Equals(md5str))
                        {
                            if (File.Exists(file))
                            {
                                File.Move(file, file.Replace(".exe", ".temp"));
                                await Task.Run(() => DownloadFile(_urlC, file));
                            }
                            System.Diagnostics.Process.Start("_Updater.exe");
                            Application.Exit();
                        }
                    }

                    dir = Path.GetDirectoryName(file);
                    if (!string.IsNullOrEmpty(dir))
                    {
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        int index = dir.Length;
                        nuevofile = file.Remove(0, index + 1);
                    }

                    if (!File.Exists(file))
                    {
                        conteo++;
                        SetProgressSafe((conteo * 100) / Maxitems);
                        if (!string.IsNullOrEmpty(dir))
                        {
                            if (Directory.Exists(dir))
                            {
                                await Task.Run(() => DownloadFile(url, $"{cfg.MinecraftDirectory}\\mods\\{file}"));
                            }
                        }
                        else
                        {
                            await Task.Run(() => DownloadFile(url, $"{cfg.MinecraftDirectory}\\mods\\{file}"));
                        }
                    }
                    else
                    {

                        await Task.Run(() => Comprobar());

                    }
                }

                SetLabel1TextSafe("Actualizado");
                SetSafeButton(btn_exit, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private async void Comprobar()
        {
            conteo++;
            SetProgressSafe((conteo * 100) / Maxitems);
            SetLabel1TextSafe("Comprobando: " + file);
            //
            string md5str;
            if (file == "_Updater.exe")
                md5str = await Task.Run(() => GetHashMD5(file));
            else
                md5str = await Task.Run(() => GetHashMD5($"{cfg.MinecraftDirectory}\\mods\\{file}"));
            //
            if (!md5.Equals(md5str))
            {
                if (!string.IsNullOrEmpty(dir))
                {
                    if (Directory.Exists(dir))
                    {
                        await Task.Run(() => DownloadFile(url, $"{cfg.MinecraftDirectory}\\mods\\{file}"));
                    }
                }
                else
                {
                    await Task.Run(() => DownloadFile(url, $"{cfg.MinecraftDirectory}\\mods\\{file}"));
                }
            }
        }

        public static string GetHashMD5(string file)
        {
            using (FileStream buffer = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                HashAlgorithm MD5hash = new MD5CryptoServiceProvider();
                byte[] hashmd5 = MD5hash.ComputeHash(buffer);
                buffer.Close();
                return BitConverter.ToString(MD5hash.ComputeHash(hashmd5), 0).Replace("-", string.Empty);
            }
        }

        public void CheckGameData(string GameData)
        {
            if (File.Exists(GameData))
            {
                if (File.Exists(OldData))
                {
                    File.Delete(OldData);
                }
                if (!File.Exists(OldData))
                {
                    File.Move(GameData, OldData);
                }
            }
        }
    }
}
