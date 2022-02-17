using LilFamStudioNet5.ViewModels.AbonementDynamicDateMustBeUsedTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Abonement
{
    public class AbonementEditViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string specialStatus { get; set; }
        public int days { get; set; }
        public int price { get; set; }
        public int visits { get; set; }
        public int status_of_visible { get; set; }
        public int status_of_deleted { get; set; }
        public int status_for_app { get; set; }
        public int is_trial { get; set; }
        public int is_private { get; set; }
        public List<AbonementDynamicDateMustBeUsedToLiteViewModel> abonementDynamicDatesMustBeUsedToLiteViewModel { get; set; }

        public LinkedList<int> dateDays { get; set; }
        public LinkedList<int> dateMonth { get; set; }
        public LinkedList<int> dateYears { get; set; }

        public AbonementEditViewModel(int id, string name, string specialStatus, int days, int price, int visits, int status_of_visible, int status_of_deleted, int status_for_app, int is_trial, int is_private, List<AbonementDynamicDateMustBeUsedToLiteViewModel> abonementDynamicDatesMustBeUsedToLiteViewModel)
        {
            this.id = id;
            this.name = name;
            this.specialStatus = specialStatus;
            this.days = days;
            this.price = price;
            this.visits = visits;
            this.status_of_visible = status_of_visible;
            this.status_of_deleted = status_of_deleted;
            this.status_for_app = status_for_app;
            this.is_trial = is_trial;
            this.is_private = is_private;
            this.abonementDynamicDatesMustBeUsedToLiteViewModel = abonementDynamicDatesMustBeUsedToLiteViewModel;

            dateDays = new LinkedList<int>();
            //String value;
            for (int i = 1; i <= 31; i++)
            {
                //value = (i < 10 ? "0" + i.ToString() : i.ToString());
                dateDays.AddLast(i);
            }
            dateMonth = new LinkedList<int>();
            for (int i = 1; i <= 12; i++)
            {
                //value = (i < 10 ? "0" + i.ToString() : i.ToString());
                dateMonth.AddLast(i);
            }
            dateYears = new LinkedList<int>();
            for (int i = DateTime.Now.Year + 1; i > 2000; i--)
            {
                dateYears.AddLast(i);
            }
        }

    }
}
