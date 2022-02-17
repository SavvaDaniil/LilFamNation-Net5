using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Abonement
{
    public class AbonementWithConnectionToDiscountLiteViewModel
    {
        public int id_of_abonement { get; set; }
        public int id_of_discount { get; set; }
        public string name_of_abonement { get; set; }
        public int status { get; set; }
        public int value { get; set; }
        public int price_of_abonement { get; set; }
        public int price_with_discount { get; set; }

        public AbonementWithConnectionToDiscountLiteViewModel(int id_of_abonement, int id_of_discount, string name_of_abonement, int status, int value, int price_of_abonement, int price_with_discount)
        {
            this.id_of_abonement = id_of_abonement;
            this.id_of_discount = id_of_discount;
            this.name_of_abonement = name_of_abonement;
            this.status = status;
            this.value = value;
            this.price_of_abonement = price_of_abonement;
            this.price_with_discount = price_with_discount;
        }
    }
}
