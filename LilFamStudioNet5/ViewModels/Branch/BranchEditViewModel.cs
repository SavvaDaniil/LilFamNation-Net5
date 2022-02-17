using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Branch
{
    public class BranchEditViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string coordinates { get; set; }

        public BranchEditViewModel(int id, string name, string description, string coordinates)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.coordinates = coordinates;
        }
    }
}
