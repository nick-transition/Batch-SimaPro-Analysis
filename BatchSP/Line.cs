using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimaPro;

namespace BatchSP
{
    class Line
    {
        private string strcontents;
        private SimaPart project;
    
        public Line(string str)
        {
            strcontents = str;
        }

        public void PumpLine(SimaPart proj){
            string[] result = strcontents.Split('%');
            // String to float conversion works on result inv- [3], std-[5]
            //float inventory = Convert.ToSingle(result[5]);
            project = proj;
            double std = Convert.ToDouble(result[5]);
            int id = Convert.ToInt32(result[0]);
            writetoSP(id,result[1],result[2],result[3],std);          
        }

        public void writetoSP(int id, string process, string item, string inventory, double stddev) 
        {
            switch (id)
            {
                case 1:
                    project.UpdateProcessMaterial(process,item,inventory,stddev);
                    break;
                case 2:
                    project.UpdateProcessAirborneEmission(process, item, inventory, stddev);
                    break;
                case 3:
                    project.UpdateProcessAvoided(process, item, inventory, stddev);
                    break;
                case 4:
                    project.UpdateProcessCalcParam(process, item, inventory, stddev);
                    break;
                case 5:
                    project.UpdateProcessProducts(process, item, inventory, stddev);
                    break;
                case 6:
                    project.UpdateWaterEmissions(process, item, inventory, stddev);
                    break;
                case 7:
                    project.UpdateProcessRawMaterial(process, item, inventory, stddev);
                    break;
                case 8:
                    int itemNum = Convert.ToInt32(item);
                    project.PositionUpdateAirborne(process, itemNum, inventory, stddev);
                    break;
                default:
                    Console.WriteLine("No method for this line type");
                    break;     
            }
 
        }
    }
}
