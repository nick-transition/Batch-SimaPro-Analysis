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
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        Line str = new Line(sr.ReadLine());
                        str.SplitString();
                    }
                }
        }
    }
}
