using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.cfg
{
    [Serializable]
    public class Config
    {
        public string MinecraftDirectory;
        public static void Write(string path, Config cnf)
        {

            using (var w = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None)))
            { w.Write(cnf.MinecraftDirectory); }
        }
        public static Config Load(string path)
        {
            using (var r = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None)))
            {
                Config cc = new Config
                {
                    MinecraftDirectory = r.ReadString()
                };
                return cc;
            }

        }
    }
}
