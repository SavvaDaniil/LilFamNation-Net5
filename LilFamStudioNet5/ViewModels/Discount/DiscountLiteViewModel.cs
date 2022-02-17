using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Discount
{
    public class DiscountLiteViewModel
    {
        public int schetchik { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? dateOfAdd { get; set; }

        public DiscountLiteViewModel(int id, string name, DateTime? dateOfAdd)
        {
            this.id = id;
            this.name = name;
            this.dateOfAdd = dateOfAdd;
        }

        public DiscountLiteViewModel(int schetchik, int id, string name, DateTime? dateOfAdd)
        {
            this.schetchik = schetchik;
            this.id = id;
            this.name = name;
            this.dateOfAdd = dateOfAdd;
        }
    }
}
