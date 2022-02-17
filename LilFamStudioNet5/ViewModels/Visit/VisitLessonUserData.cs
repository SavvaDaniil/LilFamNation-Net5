using LilFamStudioNet5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonUserData
    {
        public int id_of_user { get; set; }
        public string fio { get; set; }
        //public Dictionary<DateTime, LinkedList<VisitLessonUserVisitStatus>> userVisitsByDate { get; set; } 
        public LinkedList<DanceGroupLessonUserVisitsData> danceGroupLessonUserVisitsDatas { get; set; }

        public bool isTimeLimitConnectionToDanceGoupExceeded {get;set;}

        public VisitLessonUserData(int id_of_user, string fio)
        {
            this.id_of_user = id_of_user;
            this.fio = fio;
        }


    }
}
