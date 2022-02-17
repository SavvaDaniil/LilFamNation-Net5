using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroupDayOfWeek
{
    public class DanceGroupDayOfWeekLiteViewModel
    {
        public int id { get; set; }
        public int dayOfWeek { get; set; }
        public string dayOfWeekName { get; set; }
        public int status { get; set; }
        public TimeSpan? timeFrom { get; set; }
        public TimeSpan? timeTo { get; set; }

        public DanceGroupDayOfWeekLiteViewModel(int id, int dayOfWeek, string dayOfWeekName, int status, TimeSpan? timeFrom, TimeSpan? timeTo)
        {
            this.id = id;
            this.dayOfWeek = dayOfWeek;
            this.dayOfWeekName = dayOfWeekName;
            this.status = status;
            this.timeFrom = timeFrom;
            this.timeTo = timeTo;
        }
    }
}
