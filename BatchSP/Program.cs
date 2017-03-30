﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using SimaPro;

public static class ListExtenstions
{
    public static void AddMany<T>(this List<T> list, params T[] elements)
    {
        list.AddRange(elements);
    }
}
namespace BatchSP
{
    class Program
    {
        static void batchRun()
        {
            Console.WriteLine("Started App...");
            string direc = "D:/Dairy CAP/IFSM-Connection/temp/";

            List<string> proNames = new List<string>();
            List<double> proAmt = new List<double>();
            List<string> targets = new List<string>();

            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS-1/17");

            //Open file and stream contents
            Console.WriteLine("Updating SimaPro");
            File fileSet = new File();
            fileSet.OperateOnFile("D:/Dairy CAP/IFSM-Connection/Output/TwinBirch-Output.csv", project);

            //Set up calculation output file
            System.IO.StreamWriter fileO = new System.IO.StreamWriter("c://Users/nstoddar/Documents/calc-out.csv", false);

            // We want these values even if they return zero, so let's add them to an array and send that array to our project object
            targets.AddMany("Grass pasture, at farm/US U", "High Quality Silage, IFSM", "Low Quality Silage, IFSM", "High Quality Hay, IFSM", "Low Quality Hay, IFSM", "Grain silage, IFSM/US U",
                "High Moisture Grain", "Dry Grain, IFSM", "Purchased Grain", "Purchased Hay", "Tallow, at plant/CH WITH US ELECTRICITY U", "Degradable Crude Protein Supplement I", "Undegradable Crude Protein Supplement II",
                "Alfalfa Silage Feeding Fuel, IFSM", "Alfalfa Silage Production Fuel, IFSM", "Corn Silage Feeding Fuel, IFSM", "Corn Silage Production Fuel, IFSM", "Crop Irrigation Fuel Use, IFSM", "Farm Truck Fuel Use, IFSM",
                "Grain Feeding Fuel Use, IFSM", "Grain Harvest Fuel Use, IFSM", "Grain Harvest Fuel Use, IFSM", "Grain Planting Fuel Use, IFSM", "Hay Feeding Fuel Use, IFSM", "Hay, Straw Production Fuel",
                "High Moisture Grain Feeding Fuel, IFSM", "High Moisture Grain Production Fuel, IFSM", "Manure Handling Fuel Use, IFSM", "Pasture Maintenance Fuel, IFSM", "Loose housing system, cattle, operation/CH IFSM", "Minerals Mixture",
                "Transport, single unit truck, gasoline powered NREL /US", "Dry Cow", "Early Lactation Cow", "Late Lactation Cow", "Mid Lactation Cow", "Older Heifer", "Young Heifer");

            project.CreateProcessList(targets);

            //Run standard calc on IDXSet
            //string methodName = project.SP.get_MethodName(69);
            project.RunCalc("Base Scenario", "IPCC 2013 GWP 100a", "IPCC GWP 100a");
            int topNode = project.SP.NetworkTopNodeIndex;
            project.GetIDX(topNode);

            // Setup the output headers
            string header = "File name,";
            foreach (KeyValuePair<string, int> entry in project.idxDict)
            {
                header += "\"" + entry.Key + "\",";
            }
            fileO.WriteLine(header);

            Console.WriteLine("Headers printed");

            // Begin looping through the files in the directory
            string[] fileEntries = Directory.GetFiles(direc);

            foreach (string filename in fileEntries)
            {
                SortedDictionary<string, double> results = new SortedDictionary<string, double>();
                List<string> names = new List<string>();
                //replace SP values with those in the current simmulation output
                File file = new File();
                string shortName = Path.GetFileNameWithoutExtension(filename);

                Console.WriteLine("Starting output for {0}", shortName);
                file.OperateOnFile(filename, project);
                project.RunCalc("Base Scenario", "IPCC 2013 GWP 100a", "IPCC GWP 100a");

                int tNode = project.SP.NetworkTopNodeIndex;

                results = project.GetResults(project.SP.NetworkTopNodeIndex);
                names = project.GetNames();
                string printLine = shortName + ",";

                //Make sure we always have the same number of process being printed out as number of headers
                foreach (KeyValuePair<string, int> kvp in project.idxDict)
                {
                    if (!project.resultsDict.ContainsKey(kvp.Key))
                    {
                        project.resultsDict.Add(kvp.Key, 0);
                    }
                }

                //Stringify results
                foreach (KeyValuePair<string, double> elem in project.resultsDict)
                {
                    printLine += elem.Value + ",";
                }
                //clear results for neext calculation
                project.resultsDict.Clear();

                fileO.WriteLine(printLine);
                Console.WriteLine("Completed SimaPro results output for {0}.", shortName);

            }

            fileO.Close();
            project.Close(); 
        
        }
        static void Test()
        {
            Console.WriteLine("Test started....");
            SimaPart project = new SimaPart("DairyCAP-NS");
            string methodName = project.SP.get_MethodName(69);
            Console.WriteLine(methodName);

        }

        static void Main(string[] args)
        {
            batchRun();
            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine(); 
        }
        
        //static void testing()
        //{
        //    Console.WriteLine("Started App...");
        //    string direc = "D:/Dairy CAP/IFSM-Connection/Output/";
        //    //Open and Close SimaPro
        //    SimaPart project = new SimaPart("DairyCAP-NS");
        //    File file = new File();
        //    file.OperateOnFile(direc+"TwinBirch-Output.csv", project);
        //    project.Close();
        //    Console.WriteLine("Press <Enter> to continue...");
        //    Console.ReadLine();
        //
        //
        //}
    }
}
