using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Admin
{
    public class AdminPanelAccessLiteViewModel
    {
        public string field { get; set; }
        public int status { get; set; }
        public string name { get; set; }

        public AdminPanelAccessLiteViewModel(string field, int status, string name)
        {
            this.field = field;
            this.status = status;
            this.name = name;
        }
    }
}
