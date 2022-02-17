using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupScheduleWithNameOfDayOfWeek
    {
        public string name_of_day_of_week { get; set; }
        public List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels { get; set; }

        public DanceGroupScheduleViewModel danceGroupScheduleViewModel { get; set; }


        public DanceGroupScheduleWithNameOfDayOfWeek(string name_of_day_of_week, List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels)
        {
            this.name_of_day_of_week = name_of_day_of_week;
            this.danceGroupScheduleViewModels = danceGroupScheduleViewModels;
        }

        public DanceGroupScheduleWithNameOfDayOfWeek(string name_of_day_of_week, DanceGroupScheduleViewModel danceGroupScheduleViewModel)
        {
            this.name_of_day_of_week = name_of_day_of_week;
            this.danceGroupScheduleViewModel = danceGroupScheduleViewModel;
        }
    }
}
