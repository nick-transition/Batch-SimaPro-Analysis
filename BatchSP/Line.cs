using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSP
{
    class Line
    {
        private string strcontents;
        public Line(string str)
        {
            strcontents = str;

        }
        public void SplitString(){
            string[] result = strcontents.Split('%');
            Console.WriteLine(result[0]);
            // Act on the line by calling function on the contents of the string
            // First complete appropriate type conversions
            // The test SP update
             
        }
    }
}
