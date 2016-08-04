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
        private int identifier;
        private float inventory;
        private float stddev;
        private string process;
        private string item;
        public Line(string str)
        {
            strcontents = str;

        }
        public void SplitString(){
            string[] result = strcontents.Split('%');
            
            // String to float conversion works on result [3], [5]
            //float inventory = Convert.ToSingle(result[5]);
            Console.WriteLine(inventory);

            // Act on the line by calling function on the contents of the string
            // First complete appropriate type conversions
            // The test SP update
             
        }
        public void writetosp(int id, string process, string item, float inventory, float stddev) 
        {
 
        }
    }
}
