using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.PurchaseAbonement
{
    public class PurchaseAbonementLiteViewModel
    {
        public int id_of_purchase_abonement { get; set; }
        public string name { get; set; }
        public DateTime? dateOfBuy { get; set; }
        public DateTime? dateOfActivation { get; set; }
        public DateTime? dateOfMustBeUsedTo { get; set; }
        public int days { get; set; }
        public int price { get; set; }
        public int cashless { get; set; }
        public int visits { get; set; }
        public int visitsLeft { get; set; }
        public string specialStatus { get; set; }

        public bool isExpired { get; set; }

        public int id_of_user { get; set; }
        public string fio_of_user { get; set; }

        public PurchaseAbonementLiteViewModel(int id_of_purchase_abonement, string name, DateTime? dateOfBuy, DateTime? dateOfActivation, DateTime? dateOfMustBeUsedTo, int days, int price, int cashless, int visits, int visitsLeft, string specialStatus, bool isExpired)
        {
            this.id_of_purchase_abonement = id_of_purchase_abonement;
            this.name = name;
            this.dateOfBuy = dateOfBuy;
            this.dateOfActivation = dateOfActivation;
            this.dateOfMustBeUsedTo = dateOfMustBeUsedTo;
            this.days = days;
            this.price = price;
            this.cashless = cashless;
            this.visits = visits;
            this.visitsLeft = visitsLeft;
            this.specialStatus = specialStatus;
            this.isExpired = isExpired;
        }

        public PurchaseAbonementLiteViewModel(int id_of_purchase_abonement, string name, DateTime? dateOfBuy, DateTime? dateOfActivation, DateTime? dateOfMustBeUsedTo, int days, int price, int cashless, int visits, int visitsLeft, string specialStatus, bool isExpired, int id_of_user, string fio_of_user) : this(id_of_purchase_abonement, name, dateOfBuy, dateOfActivation, dateOfMustBeUsedTo, days, price, cashless, visits, visitsLeft, specialStatus, isExpired)
        {
            this.id_of_user = id_of_user;
            this.fio_of_user = fio_of_user;
        }







        /*
        public int id_of_purchase_abonement { get; set; }
        public string name { get; set; }
        public string dateOfBuy { get; set; }
        public string dateOfActivation { get; set; }
        public string dateOfMustBeUsedTo { get; set; }
        public int visits { get; set; }
        public int visitsLeft { get; set; }
        public string specialStatus { get; set; }

        public PurchaseAbonementLiteViewModel(int id_of_purchase_abonement, string name, string dateOfBuy, string dateOfActivation, string dateOfMustBeUsedTo, int visits, int visitsLeft, string specialStatus)
        {
            this.id_of_purchase_abonement = id_of_purchase_abonement;
            this.name = name;
            this.dateOfBuy = dateOfBuy;
            this.dateOfActivation = dateOfActivation;
            this.dateOfMustBeUsedTo = dateOfMustBeUsedTo;
            this.visits = visits;
            this.visitsLeft = visitsLeft;
            this.specialStatus = specialStatus;
        }
        */
    }
}
