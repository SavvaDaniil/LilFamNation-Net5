using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonHeaderDate
    {
        public string dateDayMonth { get; set; }
        public string dateYearMonthDay { get; set; }
        public DateTime? dateOfLesson { get; set; }

        public int id_of_dance_group_day_of_week { get; set; }
        public string timeFrom { get; set; }
        public string timeTo { get; set; }
        //public string timeFromTimeTo { get; set; }
        public bool isReplacementExistForLesson { get; set; }

        public VisitLessonHeaderDate(string dateDayMonth, string dateYearMonthDay, DateTime? dateOfLesson, int id_of_dance_group_day_of_week, string timeFrom, string timeTo, bool isReplacementExistForLesson)
        {
            this.dateDayMonth = dateDayMonth;
            this.dateYearMonthDay = dateYearMonthDay;
            this.dateOfLesson = dateOfLesson;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
            this.timeFrom = timeFrom;
            this.timeTo = timeTo;
            this.isReplacementExistForLesson = isReplacementExistForLesson;
        }
    }
}