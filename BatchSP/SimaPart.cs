using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimaPro;

namespace BatchSP
{
    class SimaPart:Project
    {

        private Process PCmaterial;

        public SimaPart(string str):base(str)
        {
        }

        public void UpdateProcessMaterial(string process,string item,string inventory,double stat) 
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_LineCount(TProcessPart.ppMaterialsFuels);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).ObjectName;
                    
                    if (ObjName == item)
                    {  
                        PCmaterial.Edit();

                        PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).Amount = inventory;
                        string dist = PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).Distribution.ToString();

                        if(dist !=  "dsNormal")
                        {
                            PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).Distribution = TDistribution.dsNormal;
                            PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).StandardDeviation = stat;
                        }
                        else
                        {
                            PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).StandardDeviation = stat;
                        }

                        //PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).StandardDeviation = stat;
                        PCmaterial.Update();
                        Console.WriteLine("Process Part: " + PCmaterial.get_Line(TProcessPart.ppMaterialsFuels, i).ObjectName + " Updated");
                    }

                }

            }
            
        }




    }
}
