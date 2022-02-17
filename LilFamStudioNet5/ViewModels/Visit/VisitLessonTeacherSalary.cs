using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonTeacherSalary
    {
        public int id_of_dance_group { get; set; }
        public int id_of_dance_group_day_of_week { get; set; }
        public string dateOfLessonStr { get; set; }
        public DateTime? dateOfLesson { get; set; }

        public int summ { get; set; }

        public VisitLessonTeacherSalary(int id_of_dance_group, int id_of_dance_group_day_of_week, string dateOfLessonStr, DateTime? dateOfLesson)
        {
            this.id_of_dance_group = id_of_dance_group;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
            this.dateOfLessonStr = dateOfLessonStr;
            this.dateOfLesson = dateOfLesson;
        }
    }
}
