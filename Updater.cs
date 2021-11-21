using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;
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
        private int conteo;
        private readonly Config cfg;
        private Utils utils;

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
                utils = new Utils(Data);
                await Task.Run(() => Gamedatas());
                lb_name.Text = "Actualizado";
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
        #region Invocacion segura
        private void SetLabel1TextSafe(string txt)
        {
            if (lb_name.InvokeRequired)
                lb_name.Invoke(new Action(() => lb_name.Text = txt));
            else
                lb_name.Text = txt;
        }
        private void SetProgressSafe(int p)
        {
            if (pb_status.InvokeRequired)
                pb_status.Invoke(new Action(() => pb_status.Value = p));
            else
                pb_status.Value = p;
        }
        private void SetLabelPercentTextSafe(int progressPercentage)
        {
            if (lb_name.InvokeRequired)
                lb_percent.Invoke(new Action(() => lb_percent.Text = $"{progressPercentage}%"));
            else
                lb_percent.Text = $"{progressPercentage}%";
        }
        private void SetSafeButton(Button btn, bool v)
        {
            btn.Invoke(new Action(() => btn.Enabled = v));
        }
        #endregion
        private void EstadoDelProgreso(object sender, DownloadProgressChangedEventArgs e)
        {
            SetProgressSafe(e.ProgressPercentage);
            SetLabelPercentTextSafe(e.ProgressPercentage);
        }
        
        private void Completado(object sender, AsyncCompletedEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate { downloadComplete = true; });
        }

        private void Gamedatas()
        {
            try
            {
                    Check();
                    SetLabel1TextSafe("Actualizado");
                    SetSafeButton(btn_exit, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetLabel1TextSafe("Error");
                SetSafeButton(btn_exit, true);
            }
        }

        private async void Check()
        {
            try
            {
                foreach (var line in utils.Files)
                {
                    string file, remotemd5, urld, md5local;
                    file = line.Key;
                    remotemd5 = line.Value;
                    urld = url + file;
                    if (file == "_Updater.exe")
                    {
                        md5local = GetHashMD5(file);

                        if (!remotemd5.Equals(md5local))
                        {
                            if (File.Exists(file))
                            {
                                File.Move(file, file.Replace(".exe", ".temp"));
                                await Task.Run(() => DownloadFile(urld, file));
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
                            await Task.Run(() => DownloadFile(url + file, $"{cfg.MinecraftDirectory}\\mods\\{file}"));
                        }
                        else
                        {
                            Comprobar(file, remotemd5);
                        }
                    }
                }
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

        private async void Comprobar(string file, string remotemd5)
        {
            conteo++;
            SetProgressSafe((conteo * 100) / utils.Length);
            SetLabel1TextSafe("Comprobando: " + file);
            string localmd5;
            if (file == "_Updater.exe")
                localmd5 = GetHashMD5(file);
            else
                localmd5 = await Task.Run(() => GetHashMD5($"{cfg.MinecraftDirectory}\\mods\\{file}"));
            if (!remotemd5.Equals(localmd5))
            {
                await Task.Run(() => DownloadFile(url + file, $"{cfg.MinecraftDirectory}\\mods\\{file}"));
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
