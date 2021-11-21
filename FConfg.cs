using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Updater.cfg;

namespace Updater
{
    public partial class FConfg : Form
    {
        readonly Config cnf = new Config();
        public FConfg()
        {
            InitializeComponent();
            txt_directory.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
        }

        private void Btn_open_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fb = new FolderBrowserDialog())
            {
                fb.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
                if (fb.ShowDialog() != DialogResult.OK) return;
                cnf.MinecraftDirectory = fb.SelectedPath;
                txt_directory.Text = fb.SelectedPath;
            }
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cnf.MinecraftDirectory))
            {
                cnf.MinecraftDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
                Config.Write("Config.dat", cnf);
            }
            else
            {
                Config.Write("Config.dat", cnf);
            }

            this.Hide();
            Updater u = new Updater();
            u.Show();
        }
    }
}
