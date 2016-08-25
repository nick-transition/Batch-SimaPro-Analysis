﻿using System;
using System.Collections.Generic;
using SimaPro;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSP
{
    class Project
    {
        public string ProjectName;
        public SimaProServer SP;
        public SortedDictionary<string, int> idxDict = new SortedDictionary<string, int>();
        public SortedDictionary <string, double> resultsDict = new SortedDictionary<string,double>();
        private List<string> TargetPro = new List<string>();
        protected Project() { }
        public Project(string name)
        {
            ProjectName = name;
            SP = new SimaProServer();
            SP.Server = "NexusDB@10.4.45.166"; // "nexusdb@192.168.1.113" if multi user 
            SP.Alias = "Default"; // "Default" if multi user
            SP.Database = "UARK_MAIN_SPv8";
            SP.OpenDatabase();
            SP.Login("Nick", ""); // values ignored for single user
            SP.OpenProject(ProjectName, "");

            Console.WriteLine("Opened database and project");
        }
        public void RunCalc(string scenario, string methodName, string shortName) {
            if (SP.Network(ProjectName, TProcessType.ptAssembly, scenario, "Methods", methodName, ""))
            {          
                SP.NetworkCalcScore(TResultType.rtCharacterisation, shortName, "", "");
                Console.WriteLine("Calculation Complete");
            }
            else
            {
                Console.WriteLine("Calculation Failure!!!!");
            }
            
        }
        public void CreateProcessList(List<string> list)
        {
            TargetPro = list;
        }
        public void GetIDX(int nodeIdx)
        {
            int numChild = SP.get_NetworkChildNodeCount(nodeIdx);
            string curPro = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, nodeIdx,0).ProductName;
            double curAmt = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, nodeIdx, 0).Amount;
            idxDict.Add(curPro, nodeIdx);
            for (int i = 0; i < numChild; i++)
            {
                int childIdx = SP.get_NetworkChildNodeIndex(nodeIdx, i);
                string childPro = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, childIdx, 0).ProductName;
                double childAmt = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, childIdx, 0).Amount;

                if (!idxDict.ContainsKey(childPro) && childIdx > 0 && childAmt > .001f || !idxDict.ContainsKey(childPro) && TargetPro.Contains(childPro))
                {
                    GetIDX(childIdx);
                }
            }

        }
        public SortedDictionary<string,double> GetResults(int nodeIdx)
        {

            int numChild = SP.get_NetworkChildNodeCount(nodeIdx);
            string curPro = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, nodeIdx, 0).ProductName;
            double curAmt = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, nodeIdx, 0).Amount;
            resultsDict.Add(curPro, curAmt);

            if (curPro == "Raw milk, at dairy farm/US IFSM")
            {
                Console.WriteLine("Raw milk Amt: {0}",curAmt);
            }
          
            for (int i = 0; i < numChild; i++)
            {
                int childIdx = SP.get_NetworkChildNodeIndex(nodeIdx, i);
                string childPro = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, childIdx, 0).ProductName;
                double childAmt = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, childIdx, 0).Amount;

                if (!resultsDict.ContainsKey(childPro) && idxDict.ContainsKey(childPro))
                {
                    GetResults(childIdx);
                }
            }

            return resultsDict;

        }
        public List<string> GetNames()
        {
            List<string> Results = new List<string>();
            foreach (KeyValuePair<string, int> elem in idxDict)
            {
                string idxResult = SP.NetworkResult(TNodeResultType.nrIndicatorTotal, elem.Value, 0).ProductName;

                Results.Add(idxResult);
            }
            return Results;

        }
        public void Close()
        {
            SP.Logout();
            SP.CloseDatabase();
            SP = null;
            Console.WriteLine("Closed SimaPro");
        }
    }
}
