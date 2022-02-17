using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupConnectionToUserAdminViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int status { get; set; }

        public DanceGroupConnectionToUserAdminViewModel(int id, string name, int status)
        {
            this.id = id;
            this.name = name;
            this.status = status;
        }
    }
}
