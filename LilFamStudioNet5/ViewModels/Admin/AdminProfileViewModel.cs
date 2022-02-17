using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Admin
{
    public class AdminProfileViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string position { get; set; }

        public AdminProfileViewModel(int id, string name, string username, string position)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.position = position;
        }
    }
}
