using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.PurchaseAbonement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitPrepareViewModel
    {
        public string nameOfUser { get; set; }
        public DateTime? date_of_buy { get; set; }
        public int id_of_dance_group { get; set; }
        public string nameOfDanceGroup { get; set; }
        public int id_of_dance_group_day_of_week { get; set; }
        public AbonementsBySpecialStatusViewModel abonementsBySpecialStatusViewModel { get; set; }
        public List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels { get; set; }
        public VisitLiteViewModel visitLiteViewModel { get; set; }
        public List<VisitLiteViewModel> visitLiteViewModels { get; set; }

        public VisitPrepareViewModel(string nameOfUser, DateTime? date_of_buy, int id_of_dance_group, string nameOfDanceGroup, int id_of_dance_group_day_of_week, AbonementsBySpecialStatusViewModel abonementsBySpecialStatusViewModel, List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels, VisitLiteViewModel visitLiteViewModel, List<VisitLiteViewModel> visitLiteViewModels)
        {
            this.nameOfUser = nameOfUser;
            this.date_of_buy = date_of_buy;
            this.id_of_dance_group = id_of_dance_group;
            this.nameOfDanceGroup = nameOfDanceGroup;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
            this.abonementsBySpecialStatusViewModel = abonementsBySpecialStatusViewModel;
            this.purchaseAbonementLiteViewModels = purchaseAbonementLiteViewModels;
            this.visitLiteViewModel = visitLiteViewModel;
            this.visitLiteViewModels = visitLiteViewModels;
        }
    }
}
