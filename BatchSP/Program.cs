using System;
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
        static void Main(string[] args)
        {
            Console.WriteLine("Started App...");
            string direc = "D:/Dairy CAP/IFSM-Connection/Output/";
            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS");
            List<string> proNames = new List<string>();
            List<double> proAmt = new List<double>();
            List<string> targets = new List<string>();
 
            //Open file and stream contents
            Console.WriteLine("Updating SimaPro");
            File fileSet = new File();
            //fileSet.OperateOnFile("D:/Dairy CAP/IFSM-Connection/IDXSet-Output.csv", project);

            //Set up calculation output file
            System.IO.StreamWriter fileO = new System.IO.StreamWriter("c://Users/nstoddar/Documents/calc-out.csv", false);

            // We want these values even if they return zero, so let's add them to an array and send that array to our project object
            targets.AddMany("Grass pasture, at farm/US U", "High Quality Silage, IFSM", "Low Quality Silage, IFSM", "High Quality Hay, IFSM", "Low Quality Hay, IFSM", "Grain silage, IFSM/US U",
                "High Moisture Grain", "Dry Grain, IFSM", "Purchased Grain", "Purchased Hay", "Tallow, at plant/CH WITH US ELECTRICITY U", "Degradable Crude Protein Supplement I","Undegradable Crude Protein Supplement II",
                "Alfalfa Silage Feeding Fuel, IFSM", "Alfalfa Silage Production Fuel, IFSM", "Corn Silage Feeding Fuel, IFSM","Corn Silage Production Fuel, IFSM", "Crop Irrigation Fuel Use, IFSM","Farm Truck Fuel Use, IFSM",
                "Grain Feeding Fuel Use, IFSM","Grain Harvest Fuel Use, IFSM","Grain Harvest Fuel Use, IFSM","Grain Planting Fuel Use, IFSM","Hay Feeding Fuel Use, IFSM","Hay, Straw Production Fuel",
                "High Moisture Grain Feeding Fuel, IFSM", "High Moisture Grain Production Fuel, IFSM", "Manure Handling Fuel Use, IFSM", "Pasture Maintenance Fuel, IFSM");

            project.CreateProcessList(targets);

            //Run standard calc on IDXSet
            project.RunCalc("Base Scenario", "IPCC 2013 GWP 100a", "IPCC GWP 100a");
            int topNode = project.SP.NetworkTopNodeIndex;
            project.GetIDX(topNode);

            // Setup the output headers
            string header = "";
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
                List<double> results = new List<double>();
                //replace SP values with those in the current simmulation output
                File file = new File();
                Console.WriteLine("Starting output for {0}",filename);
                file.OperateOnFile(filename, project);
                project.RunCalc("Base Scenario", "IPCC 2013 GWP 100a", "IPCC GWP 100a");
                
                results = project.GetResults();

                string vals = "";

                foreach (double value in results)
                {
                    vals += value + ",";
                }
   
                fileO.WriteLine(vals);
                Console.WriteLine("Completed SimaPro results output for {0}.",filename);
            }
         
            fileO.Close();
            project.Close();
            
            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine();   
        }
        
    }
}
