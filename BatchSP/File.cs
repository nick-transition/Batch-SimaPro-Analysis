using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BatchSP
{
    class File
    {

        private static string directory;
        public File(string dir)
	    {
            directory = dir;
	    }

        public void OperateOnFile(string file){
            string path = directory + file; 
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
        }
    }
}
