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
            //string direc = "D:/Dairy CAP/IFSM-Connection/";
            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS");
            List<string> proNames = new List<string>();
            List<double> proAmt = new List<double>();
            List<string> targets = new List<string>();
 
            //Open file and stream contents
            Console.WriteLine("Updating SimePro");
            //File file = new File(direc);
            //file.OperateOnFile(direc+"TwinBirch-Output.csv",project);
            targets.AddMany("Grass pasture, at farm/US U", "High Quality Silage, IFSM", "Low Quality Silage, IFSM", "High Quality Hay, IFSM", "Low Quality Hay, IFSM", "Grain silage, IFSM/US U",
                "High Moisture Grain", "Dry Grain, IFSM", "Purchased Grain", "Purchased Hay", "Tallow, at plant/CH WITH US ELECTRICITY U", "Degradable Crude Protein Supplement I","Undegradable Crude Protein Supplement II",
                "Alfalfa Silage Feeding Fuel, IFSM", "Alfalfa Silage Production Fuel, IFSM", "Corn Silage Feeding Fuel, IFSM","Corn Silage Production Fuel, IFSM", "Crop Irrigation Fuel Use, IFSM","Farm Truck Fuel Use, IFSM",
                "Grain Feeding Fuel Use, IFSM","Grain Harvest Fuel Use, IFSM","Grain Harvest Fuel Use, IFSM","Grain Planting Fuel Use, IFSM","Hay Feeding Fuel Use, IFSM","Hay, Straw Production Fuel",
                "High Moisture Grain Feeding Fuel, IFSM", "High Moisture Grain Production Fuel, IFSM", "Manure Handling Fuel Use, IFSM", "Pasture Maintenance Fuel, IFSM");

            project.CreateProcessList(targets);

            project.RunCalc("Base Scenario", "IPCC 2013 GWP 100a", "IPCC GWP 100a");
            int topNode = project.SP.NetworkTopNodeIndex;
            project.GetIDX(topNode);
            string header = "";
            string vals = "";
            foreach(KeyValuePair<string,int> entry in project.idxDict)
            {              
                header += "\""+entry.Key+"\",";
            }
            foreach (KeyValuePair<string, int> entry in project.idxDict)
            {
                vals += entry.Value+",";
            }

            System.IO.StreamWriter file = new System.IO.StreamWriter("c://Users/nstoddar/Documents/calc-out.csv", false);
            file.WriteLine(header);
            file.WriteLine(vals);
            file.Close();

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
