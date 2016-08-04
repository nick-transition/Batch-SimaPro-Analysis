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
            //string direc = "D:/Dairy CAP/IFSM-Connection/";
            
            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS");

            project.UpdateProcessMaterial("Raw milk, at dairy farm/US IFSM", "Farm Fuel Use, IFSM", "1", 0);
            project.UpdateProcessMaterial("Raw milk, at dairy farm/US IFSM", "Dry Cow", "225", 12);

            project.Close();

            //Open file and stream contents
            //File directory = new File(direc);
            //directory.OperateOnFile(direc+"TwinBirch-Output.csv");

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
