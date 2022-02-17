using LilFamStudioNet5.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.TeacherReplacement
{
    public class TeacherReplacementStatusViewModel
    {
        public int id_of_teacher_replace { get; set; }
        public List<TeacherLiteViewModel> teacherLiteViewModels { get; set; }

        public DateTime? dateOfDay { get; set; }
        public string name_of_dance_group {get;set; }
        public string name_of_dance_group_day_of_week { get; set; }

        public TeacherReplacementStatusViewModel(int id_of_teacher_replace, List<TeacherLiteViewModel> teacherLiteViewModels, DateTime? dateOfDay, string name_of_dance_group, string name_of_dance_group_day_of_week)
        {
            this.id_of_teacher_replace = id_of_teacher_replace;
            this.teacherLiteViewModels = teacherLiteViewModels;
            this.dateOfDay = dateOfDay;
            this.name_of_dance_group = name_of_dance_group;
            this.name_of_dance_group_day_of_week = name_of_dance_group_day_of_week;
        }
    }
}
