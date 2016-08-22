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

        public File()
	    {
            
	    }

        public void OperateOnFile(string file,SimaPart project){ 
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() >= 0)
                    {
                        Line str = new Line(sr.ReadLine());
                        str.PumpLine(project);
                    }
                }
        }
    }
}
