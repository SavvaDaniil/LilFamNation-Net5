using LilFamStudioNet5.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Models
{
    public class DanceGroupLesson
    {
        public string dateOfLesson { get; set; }
        public int id_of_dance_group_day_of_week { get; set; }

        public DanceGroupLesson(string dateOfLesson, int id_of_dance_group_day_of_week)
        {
            this.dateOfLesson = dateOfLesson;
            this.id_of_dance_group_day_of_week = id_of_dance_group_day_of_week;
        }



        //DateTime? dateOfLesson { get; set; }
        //DanceGroupDayOfWeek danceGroupDayOfWeek { get; set; }


    }
}
