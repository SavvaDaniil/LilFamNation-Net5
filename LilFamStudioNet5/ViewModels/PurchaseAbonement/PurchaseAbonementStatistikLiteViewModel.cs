using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.PurchaseAbonement
{
    public class PurchaseAbonementStatistikLiteViewModel
    {
        public DateTime? date_of_buy { get; set; }
        public int id_of_dance_group { get; set; }
        public string nameOfDanceGroup { get; set; }

        public List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels { get; set; }
        public int summAll { get; set; }

        public PurchaseAbonementStatistikLiteViewModel(DateTime? date_of_buy, int id_of_dance_group, string nameOfDanceGroup, List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels, int summAll)
        {
            this.date_of_buy = date_of_buy;
            this.id_of_dance_group = id_of_dance_group;
            this.nameOfDanceGroup = nameOfDanceGroup;
            this.purchaseAbonementLiteViewModels = purchaseAbonementLiteViewModels;
            this.summAll = summAll;
        }
    }
}
