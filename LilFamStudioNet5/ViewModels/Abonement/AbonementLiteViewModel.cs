using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Abonement
{
    public class AbonementLiteViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string special_status { get; set; }
        public int days { get; set; }
        public int price { get; set; }
        public int visits { get; set; }
        public int status_of_visible { get; set; }
        public int status_for_app { get; set; }
        public int is_private { get; set; }
        public int is_trial { get; set; }

        public AbonementLiteViewModel(int id, string name, string special_status, int days, int price, int visits, int status_of_visible, int status_for_app, int is_private, int is_trial)
        {
            this.id = id;
            this.name = name;
            this.special_status = special_status;
            this.days = days;
            this.price = price;
            this.visits = visits;
            this.status_of_visible = status_of_visible;
            this.status_for_app = status_for_app;
            this.is_private = is_private;
            this.is_trial = is_trial;
        }
    }
}
