using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.ConnectionAbonementToDiscount
{
    public class ConnectionAbonementToDiscountLiteViewModel
    {
        public int id_of_abonement { get; set; }
        public string name_of_abonement { get; set; }
        public int status { get; set; }
        public int value { get; set; }

        public ConnectionAbonementToDiscountLiteViewModel(int id_of_abonement, string name_of_abonement, int status, int value)
        {
            this.id_of_abonement = id_of_abonement;
            this.name_of_abonement = name_of_abonement;
            this.status = status;
            this.value = value;
        }
    }
}
