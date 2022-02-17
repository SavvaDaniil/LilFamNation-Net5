using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonUserVisitStatus
    {
        public int id_of_visit { get; set; }
        public int visitLeft { get; set; }

        public int isReservation { get; set; }
        public int statusOfReservation { get; set; }

        public VisitLessonUserVisitStatus(int id_of_visit, int visitLeft, int isReservation, int statusOfReservation)
        {
            this.id_of_visit = id_of_visit;
            this.visitLeft = visitLeft;
            this.isReservation = isReservation;
            this.statusOfReservation = statusOfReservation;
        }
    }
}
