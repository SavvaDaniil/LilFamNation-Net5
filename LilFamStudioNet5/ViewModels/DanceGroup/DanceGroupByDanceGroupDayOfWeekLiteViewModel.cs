using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupByDanceGroupDayOfWeekLiteViewModel
    {
        public int id_of_dance_group { get; set; }
        public int id_of_dance_group_day_of_week { get; set; }
        public string name { get; set; }

        public DanceGroupByDanceGroupDayOfWeekLiteViewModel(int id_of_dance_group, int id_of_dance_group_day_of_week, string name)
        {
            this.id_of_dance_group = id_of_dance_group;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
            this.name = name;
        }
    }
}
