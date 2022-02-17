using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupScheduleViewModel
    {
        public int id_of_dance_group { get; set; }
        public string name_of_dance_group { get; set; }
        public int id_of_dance_group_day_of_week { get; set; }
        public int dayOfWeek { get; set; }
        public string time_from {get;set;}
        public string time_to { get; set; }
        public string date_of_day { get; set; }
        public int id_of_teacher { get; set; }
        public string teacher_name { get; set; }
        public int id_of_branch { get; set; }
        public string name_of_branch { get; set; }
        public int statusOfCreative { get; set; }
        public int isActiveReservation { get; set; }
        public int isCansel { get; set; }

        public bool isReplacementExistForLesson { get; set; }

        public string description { get; set; }
        public string coordinates_of_branch { get; set; }
        public int id_of_teacher_replacement { get; set; }
        public string name_of_teacher_replacement { get; set; }
        public string teacherPosterSrc { get; set; }

        public DanceGroupScheduleViewModel(int id_of_dance_group, string name_of_dance_group, int id_of_dance_group_day_of_week, int dayOfWeek, string time_from, string time_to, string date_of_day, int id_of_teacher, string teacher_name, int id_of_branch, string name_of_branch, int statusOfCreative, int isActiveReservation, int isCansel, bool isReplacementExistForLesson)
        {
            this.id_of_dance_group = id_of_dance_group;
            this.name_of_dance_group = name_of_dance_group;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
            this.dayOfWeek = dayOfWeek;
            this.time_from = time_from;
            this.time_to = time_to;
            this.date_of_day = date_of_day;
            this.id_of_teacher = id_of_teacher;
            this.teacher_name = teacher_name;
            this.id_of_branch = id_of_branch;
            this.name_of_branch = name_of_branch;
            this.statusOfCreative = statusOfCreative;
            this.isActiveReservation = isActiveReservation;
            this.isCansel = isCansel;
            this.isReplacementExistForLesson = isReplacementExistForLesson;
        }

        public DanceGroupScheduleViewModel(int id_of_dance_group, string name_of_dance_group, int id_of_dance_group_day_of_week, int dayOfWeek, string time_from, string time_to, string date_of_day, int id_of_teacher, string teacher_name, int id_of_branch, string name_of_branch, int statusOfCreative, int isActiveReservation, int isCansel, bool isReplacementExistForLesson, string description, string coordinates_of_branch, int id_of_teacher_replacement, string name_of_teacher_replacement, string teacherPosterSrc) : this(id_of_dance_group, name_of_dance_group, id_of_dance_group_day_of_week, dayOfWeek, time_from, time_to, date_of_day, id_of_teacher, teacher_name, id_of_branch, name_of_branch, statusOfCreative, isActiveReservation, isCansel, isReplacementExistForLesson)
        {
            this.description = description;
            this.coordinates_of_branch = coordinates_of_branch;
            this.id_of_teacher_replacement = id_of_teacher_replacement;
            this.name_of_teacher_replacement = name_of_teacher_replacement;
            this.teacherPosterSrc = teacherPosterSrc;
        }
    }
}
