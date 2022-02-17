using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.Branch;
using LilFamStudioNet5.ViewModels.DanceGroupDayOfWeek;
using LilFamStudioNet5.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupEditViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        
        public TeacherLiteViewModel teacherLiteViewModel { get; set; }
        public List<TeacherLiteViewModel> teacherLiteViewModels { get; set; }
        public string description { get; set; }

        public BranchLiteViewModel branchLiteViewModel { get; set; }
        public List<BranchLiteViewModel> branchLiteViewModels { get; set; }

        public int status { get; set; }
        public int isEvent { get; set; }
        public int statusOfApp { get; set; }
        public int isCreative { get; set; }
        public int isActiveReservation { get; set; }
        public int isAbonementsAllowAll { get; set; }

        public List<(int, string)> daysOfWeek { get; }
        public List<DanceGroupDayOfWeekLiteViewModel> danceGroupDayOfWeekLiteViewModels { get; set; }
        public List<AbonementLiteViewModel> abonementLiteViewModels { get; set; }
        public List<int> listIdOfConnectedAbonementsToDanceGroup { get; set; }


        public DanceGroupEditViewModel(int id, string name, TeacherLiteViewModel teacherLiteViewModel, List<TeacherLiteViewModel> teacherLiteViewModels, string description, BranchLiteViewModel branchLiteViewModel, List<BranchLiteViewModel> branchLiteViewModels, int status, int isEvent, int statusOfApp, int isCreative, int isActiveReservation, int isAbonementsAllowAll, List<DanceGroupDayOfWeekLiteViewModel> danceGroupDayOfWeekLiteViewModels, List<AbonementLiteViewModel> abonementLiteViewModels, List<int> listIdOfConnectedAbonementsToDanceGroup)
        {
            this.id = id;
            this.name = name;
            this.teacherLiteViewModel = teacherLiteViewModel;
            this.teacherLiteViewModels = teacherLiteViewModels;
            this.description = description;
            this.branchLiteViewModel = branchLiteViewModel;
            this.branchLiteViewModels = branchLiteViewModels;
            this.status = status;
            this.isEvent = isEvent;
            this.statusOfApp = statusOfApp;
            this.isCreative = isCreative;
            this.isActiveReservation = isActiveReservation;
            this.isAbonementsAllowAll = isAbonementsAllowAll;
            this.danceGroupDayOfWeekLiteViewModels = danceGroupDayOfWeekLiteViewModels;
            this.abonementLiteViewModels = abonementLiteViewModels;
            this.listIdOfConnectedAbonementsToDanceGroup = listIdOfConnectedAbonementsToDanceGroup;

            daysOfWeek = new List<(int, string)>();
            daysOfWeek.Add((1, "Понедельник"));
            daysOfWeek.Add((2, "Вторник"));
            daysOfWeek.Add((3, "Среда"));
            daysOfWeek.Add((4, "Четверг"));
            daysOfWeek.Add((5, "Пятница"));
            daysOfWeek.Add((6, "Суббота"));
            daysOfWeek.Add((0, "Воскресенье"));
        }
    }
}
