using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class Utils
    {
        public Dictionary<string,string> Files { get; set; }
        public int Length { get; set; }
        public Utils(string file)
        {
            Files = new Dictionary<string, string>();
            StreamReader stream = new StreamReader(file);
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                string[] array = line.Split(char.Parse(":"));
                Files.Add(array[0], array[1]);
                Length++;

            }
        }
    }
}
