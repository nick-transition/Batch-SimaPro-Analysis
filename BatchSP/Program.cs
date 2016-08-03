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
            string direc = "D:/Dairy CAP/IFSM-Connection/";
            
            //Open and Close SimaPro
            //Project project = new Project("DairyCAP-NS");
            //project.Close();

            //Open file and stream contents
            //File directory = new File(direc);
            //directory.OperateOnFile("TwinBirch-Output.csv");

            string[] fileEntries = Directory.GetFiles(direc);
            foreach (string fileName in fileEntries)
                Console.WriteLine(fileName);

            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine();
           
            
        }
    }
}
