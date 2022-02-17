using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.User
{
    public class UserLiteViewModel
    {
        public int id { get; set; }
        public string fio { get; set; }

        public UserLiteViewModel(int id, string fio)
        {
            this.id = id;
            this.fio = fio;
        }
    }
}
