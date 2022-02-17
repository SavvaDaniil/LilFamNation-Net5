using LilFamStudioNet5.ViewModels.TeacherSalary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitStatisticLiteViewModel
    {
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public int id_of_dance_group { get; set; }
        public string nameOfDanceGroup { get; set; }

        public List<VisitAnalyticsLiteViewModel> visitAnalyticsLiteViewModels { get; set; }
        public TeacherSalaryLiteViewModel teacherSalaryLiteViewModel { get; set; }

        public VisitStatisticLiteViewModel(DateTime? dateFrom, DateTime? dateTo, int id_of_dance_group, string nameOfDanceGroup, List<VisitAnalyticsLiteViewModel> visitAnalyticsLiteViewModels, TeacherSalaryLiteViewModel teacherSalaryLiteViewModel)
        {
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.id_of_dance_group = id_of_dance_group;
            this.nameOfDanceGroup = nameOfDanceGroup;
            this.visitAnalyticsLiteViewModels = visitAnalyticsLiteViewModels;
            this.teacherSalaryLiteViewModel = teacherSalaryLiteViewModel;
        }
    }
}
