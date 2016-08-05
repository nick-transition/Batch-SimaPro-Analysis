﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using SimaPro;

namespace BatchSP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started App...");
            //string direc = "D:/Dairy CAP/IFSM-Connection/";
            
            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS");

            //Open file and stream contents
            Console.WriteLine("Updating SimePro");
            //File file = new File(direc);
            //file.OperateOnFile(direc+"TwinBirch-Output.csv",project);

            //Run Calculation
            // project.SP.get_MethodName(1) = GWP 100a
            //project.SP.Analyse("DairyCAP-NS", TProcessType.ptMaterial, "Raw milk, at dairy farm/US IFSM", "IPCC 2013 GWP 100a", "V1.01","StandardCalc");

            //int numNodes = project.SP.get_NetworkChildNodeCount(0);

            string methodName = project.SP.get_MethodName(1);
            //Console.WriteLine(project.SP.get_ResultIndicatorName(TResultType.rtCharacterisation, 1));
            if (project.SP.Analyse("DairyCap-NS", TProcessType.ptMaterial, "Raw milk, at dairy farm/US IFSM","", methodName,"")) {
                for(int i = 0; i<10; i++){
                    Console.WriteLine(project.SP.AnalyseResult(TResultType.rtCharacterisation,i));
                }     
            }

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
