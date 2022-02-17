using LilFamStudioNet5.ViewModels.PurchaseAbonement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLiteViewModel
    {
        public int id_of_visit { get; set; }
        public DateTime? date_of_buy { get; set; }
        public DateTime? date_of_add { get; set; }

        public int id_of_dance_group { get; set; }
        public string name_of_dance_group { get; set; }

        public int id_of_dance_group_day_of_week { get; set; }
        public string time_from { get; set; }
        public string time_to { get; set; }

        public int id_of_purchase_abonement { get; set; }
        public string name_of_abonement { get; set; }

        public int isAddByApp { get; set; }
        public int isReservation { get; set; }
        public int statusOfReservation { get; set; }

        public int statusForDisableCanselByUser { get; set; }

        public int id_of_user { get; set; }
        public string fio_of_user { get; set; }

        public PurchaseAbonementLiteViewModel purchaseAbonementLiteViewModel { get; set; }

        public VisitLiteViewModel(int id_of_visit, DateTime? date_of_buy, DateTime? date_of_add, int id_of_dance_group, string name_of_dance_group, int id_of_dance_group_day_of_week, string time_from, string time_to, int id_of_purchase_abonement, string name_of_abonement, int isAddByApp, int isReservation, int statusOfReservation, int statusForDisableCanselByUser)
        {
            this.id_of_visit = id_of_visit;
            this.date_of_buy = date_of_buy;
            this.date_of_add = date_of_add;
            this.id_of_dance_group = id_of_dance_group;
            this.name_of_dance_group = name_of_dance_group;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
            this.time_from = time_from;
            this.time_to = time_to;
            this.id_of_purchase_abonement = id_of_purchase_abonement;
            this.name_of_abonement = name_of_abonement;
            this.isAddByApp = isAddByApp;
            this.isReservation = isReservation;
            this.statusOfReservation = statusOfReservation;
            this.statusForDisableCanselByUser = statusForDisableCanselByUser;
        }

        public VisitLiteViewModel(int id_of_visit, DateTime? date_of_buy, DateTime? date_of_add, int id_of_dance_group, string name_of_dance_group, int id_of_dance_group_day_of_week, string time_from, string time_to, int id_of_purchase_abonement, string name_of_abonement, int isAddByApp, int isReservation, int statusOfReservation, int statusForDisableCanselByUser, int id_of_user, string fio_of_user) : this(id_of_visit, date_of_buy, date_of_add, id_of_dance_group, name_of_dance_group, id_of_dance_group_day_of_week, time_from, time_to, id_of_purchase_abonement, name_of_abonement, isAddByApp, isReservation, statusOfReservation, statusForDisableCanselByUser)
        {
            this.id_of_user = id_of_user;
            this.fio_of_user = fio_of_user;
        }
    }
}
