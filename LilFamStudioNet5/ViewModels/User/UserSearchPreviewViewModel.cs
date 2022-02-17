using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.User
{
    public class UserSearchPreviewViewModel
    {
        public int id { get; set; }
        public string fio { get; set; }
        public string phone { get; set; }
        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfLastVisit { get; set; }

        public UserSearchPreviewViewModel(int id, string fio, string phone, DateTime? dateOfAdd, DateTime? dateOfLastVisit)
        {
            this.id = id;
            this.fio = fio;
            this.phone = phone;
            this.dateOfAdd = dateOfAdd;
            this.dateOfLastVisit = dateOfLastVisit;
        }

    }

}
