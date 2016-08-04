using System;
using System.Collections.Generic;
using SimaPro;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSP
{
    class Project
    {
        protected string ProjectName;
        public SimaProServer SP;
        protected Project() { }
        //public virtual void UpdateProcessMaterial();
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
        public void Close()
        {
            SP.Logout();
            SP.CloseDatabase();
            SP = null;
            Console.WriteLine("Closed SimaPro");
        }
        //Declare function for writing to SP project
        //Declare function for running calculation on SP
        //Declare function for getting calc results
    }
}
