using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace BatchSP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started App...");
            string direc = "D:/Dairy CAP/IFSM-Connection/";
            
            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS");

            //Open file and stream contents
            Console.WriteLine("Updating SimePro");
            File file = new File(direc);
            file.OperateOnFile(direc+"TwinBirch-Output.csv",project);

            project.Close();
            //string[] fileEntries = Directory.GetFiles(direc);
            //foreach (string fileName in fileEntries)
            //    //1.Operate on the string
            //    //2.Run the calculations
            //    //3.Export the results
            //    //writes all file names in directory to a console
            //    Console.WriteLine(fileName);
            
            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine();
           
            
        }
    }
}
