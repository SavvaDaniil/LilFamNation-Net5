using LilFamStudioNet5.ViewModels.PurchaseAbonement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitAnalyticsLiteViewModel
    {
        public int id_of_visit { get; set; }
        public DateTime? date_of_buy { get; set; }
        public DateTime? date_of_add { get; set; }

        public int id_of_dance_group { get; set; }
        public string name_of_dance_group { get; set; }

        public int id_of_user { get; set; }
        public string fio_of_user { get; set; }

        public int visitsLeftOnVisit { get; set; }

        public PurchaseAbonementLiteViewModel purchaseAbonementLiteViewModel { get; set; }

        public VisitAnalyticsLiteViewModel(int id_of_visit, DateTime? date_of_buy, DateTime? date_of_add, int id_of_dance_group, string name_of_dance_group, int id_of_user, string fio_of_user, int visitsLeftOnVisit, PurchaseAbonementLiteViewModel purchaseAbonementLiteViewModel)
        {
            this.id_of_visit = id_of_visit;
            this.date_of_buy = date_of_buy;
            this.date_of_add = date_of_add;
            this.id_of_dance_group = id_of_dance_group;
            this.name_of_dance_group = name_of_dance_group;
            this.id_of_user = id_of_user;
            this.fio_of_user = fio_of_user;
            this.visitsLeftOnVisit = visitsLeftOnVisit;
            this.purchaseAbonementLiteViewModel = purchaseAbonementLiteViewModel;
        }
    }
}
