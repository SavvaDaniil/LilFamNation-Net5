using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Branch
{
    public class BranchLiteViewModel
    {
        public int schetchik { get; set; }
        public int id {get;set;}
        public string name { get; set; }
        public string coordinates { get; set; }

        public BranchLiteViewModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public BranchLiteViewModel(int id, string name, string coordinates)
        {
            this.id = id;
            this.name = name;
            this.coordinates = coordinates;
        }

        public BranchLiteViewModel(int schetchik, int id, string name, string coordinates)
        {
            this.schetchik = schetchik;
            this.id = id;
            this.name = name;
            this.coordinates = coordinates;
        }
    }
}
