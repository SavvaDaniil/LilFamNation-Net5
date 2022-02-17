using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.PurchaseAbonement
{
    public class PurchaseAbonementEditViewModel
    {
        public int id { get; set; }
        public DateTime? dateOfBuy { get; set; }
        public string specialStatus { get; set; }
        public int idOfAbonement { get; set; }
        public string nameOfAbonement { get; set; }
        public int days { get; set; }
        public DateTime? dateOfActivation { get; set; }
        public DateTime? dateMustBeUsedTo { get; set; }
        public int visits { get; set; }
        public int visitsLeft { get; set; }
        public int visitsUsed { get; set; }
        public int price { get; set; }
        public int cashless { get; set; }
        public string comment { get; set; }

        public LinkedList<int> dateDays { get; set; }
        public LinkedList<int> dateMonth { get; set; }
        public LinkedList<int> dateYears { get; set; }

        public PurchaseAbonementEditViewModel(int id, DateTime? dateOfBuy, string specialStatus, int idOfAbonement, string nameOfAbonement, int days, DateTime? dateOfActivation, DateTime? dateMustBeUsedTo, int visits, int visitsLeft, int visitsUsed, int price, int cashless, string comment)
        {
            this.id = id;
            this.dateOfBuy = dateOfBuy;
            this.specialStatus = specialStatus;
            this.idOfAbonement = idOfAbonement;
            this.nameOfAbonement = nameOfAbonement;
            this.days = days;
            this.dateOfActivation = dateOfActivation;
            this.dateMustBeUsedTo = dateMustBeUsedTo;
            this.visits = visits;
            this.visitsLeft = visitsLeft;
            this.visitsUsed = visitsUsed;
            this.price = price;
            this.cashless = cashless;
            this.comment = comment;

            dateDays = new LinkedList<int>();
            for (int i = 1; i <= 31; i++)
            {
                dateDays.AddLast(i);
            }
            dateMonth = new LinkedList<int>();
            for (int i = 1; i <= 12; i++)
            {
                dateMonth.AddLast(i);
            }
            dateYears = new LinkedList<int>();
            for (int i = DateTime.Now.Year + 1; i > 2000; i--)
            {
                dateYears.AddLast(i);
            }
        }


        /*
        public int id { get; set; }
        public string dateOfBuy { get; set; }
        public string specialStatus { get; set; }
        public int idOfAbonement { get; set; }
        public string nameOfAbonement { get; set; }
        public int days { get; set; }
        public string dateOfActivation { get; set; }
        public string dateMustBeUsedTo { get; set; }
        public int visits { get; set; }
        public int visitsLeft { get; set; }
        public int visitsUsed { get; set; }
        public int price { get; set; }
        public int cashless { get; set; }
        public string comment { get; set; }

        public LinkedList<int> dateDays { get; set; }
        public LinkedList<int> dateMonth { get; set; }
        public LinkedList<int> dateYears { get; set; }

        public PurchaseAbonementEditViewModel(int id, string dateOfBuy, string specialStatus, int idOfAbonement, string nameOfAbonement, int days, string dateOfActivation, string dateMustBeUsedTo, int visits, int visitsLeft, int visitsUsed, int price, int cashless, string comment)
        {
            this.id = id;
            this.dateOfBuy = dateOfBuy;
            this.specialStatus = specialStatus;
            this.idOfAbonement = idOfAbonement;
            this.nameOfAbonement = nameOfAbonement;
            this.days = days;
            this.dateOfActivation = dateOfActivation;
            this.dateMustBeUsedTo = dateMustBeUsedTo;
            this.visits = visits;
            this.visitsLeft = visitsLeft;
            this.visitsUsed = visitsUsed;
            this.price = price;
            this.cashless = cashless;
            this.comment = comment;

            dateDays = new LinkedList<int>();
            for (int i = 1; i <= 31; i++)
            {
                dateDays.AddLast(i);
            }
            dateMonth = new LinkedList<int>();
            for (int i = 1; i <= 12; i++)
            {
                dateMonth.AddLast(i);
            }
            dateYears = new LinkedList<int>();
            for (int i = DateTime.Now.Year + 1; i > 2000; i--)
            {
                dateYears.AddLast(i);
            }
        }
        */
    }
}
