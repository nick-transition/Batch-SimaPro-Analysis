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
                    }

                }

            }
            
        }

        public void UpdateProcessAirborneEmission(string process, string item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_LineCount(TProcessPart.ppAirborneEmissions);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_Line(TProcessPart.ppAirborneEmissions, i).ObjectName;

                    if (ObjName == item)
                    {
                        PCmaterial.Edit();

                        PCmaterial.get_Line(TProcessPart.ppAirborneEmissions , i).Amount = inventory;
                        string dist = PCmaterial.get_Line(TProcessPart.ppAirborneEmissions, i).Distribution.ToString();

                        if (dist != "dsNormal")
                        {
                            PCmaterial.get_Line(TProcessPart.ppAirborneEmissions, i).Distribution = TDistribution.dsNormal;
                            PCmaterial.get_Line(TProcessPart.ppAirborneEmissions, i).StandardDeviation = stat;
                        }
                        else
                        {
                            PCmaterial.get_Line(TProcessPart.ppAirborneEmissions, i).StandardDeviation = stat;
                        }

                        PCmaterial.Update();
                   }

                }

            }
        }

        public void UpdateProcessAvoided(string process, string item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_LineCount(TProcessPart.ppAvoidedProducts);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_Line(TProcessPart.ppAvoidedProducts, i).ObjectName;

                    if (ObjName == item)
                    {
                        PCmaterial.Edit();

                        PCmaterial.get_Line(TProcessPart.ppAvoidedProducts, i).Amount = inventory;
                        string dist = PCmaterial.get_Line(TProcessPart.ppAvoidedProducts, i).Distribution.ToString();

                        if (dist != "dsNormal")
                        {
                            PCmaterial.get_Line(TProcessPart.ppAvoidedProducts, i).Distribution = TDistribution.dsNormal;
                            PCmaterial.get_Line(TProcessPart.ppAvoidedProducts, i).StandardDeviation = stat;
                        }
                        else
                        {
                            PCmaterial.get_Line(TProcessPart.ppAvoidedProducts, i).StandardDeviation = stat;
                        }

                        PCmaterial.Update();
                    }

                }

            }
        }

        public void UpdateProcessCalcParam(string process, string item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_ParamLineCount(TParameterType.ptCalculatedParameter);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_ParamLine(TParameterType.ptCalculatedParameter, i).Name;

                    if (ObjName == item)
                    {
                        PCmaterial.Edit();
                        PCmaterial.get_ParamLine(TParameterType.ptCalculatedParameter, i).Expression = inventory;
                        PCmaterial.Update();
                    }
                }
            }
        }

        public void UpdateProcessProducts(string process, string item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_LineCount(TProcessPart.ppProducts);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_Line(TProcessPart.ppProducts, i).ObjectName;

                    if (ObjName == item)
                    {
                        PCmaterial.Edit();
                        PCmaterial.get_Line(TProcessPart.ppProducts, i).Amount = inventory;
                        PCmaterial.Update();
                    }
                }
            }
        }

        public void UpdateWaterEmissions(string process, string item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_LineCount(TProcessPart.ppWaterborneEmissions);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_Line(TProcessPart.ppWaterborneEmissions, i).ObjectName;

                    if (ObjName == item)
                    {
                        PCmaterial.Edit();

                        PCmaterial.get_Line(TProcessPart.ppWaterborneEmissions, i).Amount = inventory;
                        string dist = PCmaterial.get_Line(TProcessPart.ppWaterborneEmissions, i).Distribution.ToString();

                        if (dist != "dsNormal")
                        {
                            PCmaterial.get_Line(TProcessPart.ppWaterborneEmissions, i).Distribution = TDistribution.dsNormal;
                            PCmaterial.get_Line(TProcessPart.ppWaterborneEmissions, i).StandardDeviation = stat;
                        }
                        else
                        {
                            PCmaterial.get_Line(TProcessPart.ppWaterborneEmissions, i).StandardDeviation = stat;
                        }
                        PCmaterial.Update();
                    }
                }
            }
        }

        public void UpdateProcessRawMaterial(string process, string item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {

                int maxI = PCmaterial.get_LineCount(TProcessPart.ppRawMaterials);

                for (int i = 0; i < maxI; i++)
                {

                    string ObjName = PCmaterial.get_Line(TProcessPart.ppRawMaterials, i).ObjectName;

                    if (ObjName == item)
                    {
                        PCmaterial.Edit();

                        PCmaterial.get_Line(TProcessPart.ppRawMaterials, i).Amount = inventory;
                        string dist = PCmaterial.get_Line(TProcessPart.ppRawMaterials, i).Distribution.ToString();

                        if (dist != "dsNormal")
                        {
                            PCmaterial.get_Line(TProcessPart.ppRawMaterials, i).Distribution = TDistribution.dsNormal;
                            PCmaterial.get_Line(TProcessPart.ppRawMaterials, i).StandardDeviation = stat;
                        }
                        else
                        {
                            PCmaterial.get_Line(TProcessPart.ppRawMaterials, i).StandardDeviation = stat;
                        }
                        PCmaterial.Update();
                    }
                }
            }
        }

        public void PositionUpdateAirborne(string process, int item, string inventory, double stat)
        {
            if (SP.FindProcess(ProjectName, TProcessType.ptMaterial, process, out PCmaterial))
            {
                PCmaterial.Edit();
                PCmaterial.get_Line(TProcessPart.ppAirborneEmissions, item).Amount = inventory;
                PCmaterial.Update();          
            }

        }

    }
}
