using System;
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
            Process pro;
            //Open and Close SimaPro
            SimaPart project = new SimaPart("DairyCAP-NS");

            //Open file and stream contents
            Console.WriteLine("Updating SimePro");
            //File file = new File(direc);
            //file.OperateOnFile(direc+"TwinBirch-Output.csv",project);

            //int numNodes = project.SP.get_NetworkChildNodeCount(0);

            string methodName = project.SP.get_MethodName(1);            

            if (project.SP.Network("DairyCap-NS", TProcessType.ptAssembly, "Base Scenario","Methods",methodName,""))
            {

                project.SP.NetworkCalcScore(TResultType.rtCharacterisation, "IPCC GWP 100a", "", "");
                int uBound = project.SP.NetworkTopNodeIndex - 1;
                for (int i = 0; i < uBound; i++)
                {
                    SimaProNetworkResult Res = project.SP.NetworkResult(TNodeResultType.nrIndicatorContribution,project.SP.get_NetworkChildNodeIndex(project.SP.NetworkTopNodeIndex,i),0);
                    Console.WriteLine("Product Name:{0}\tAmount:{1}\tUnit:{2}",Res.ProductName,Res.Amount,Res.UnitName);
                }
                
            }
            else
            {
                Console.WriteLine("Failure!!!!");
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
