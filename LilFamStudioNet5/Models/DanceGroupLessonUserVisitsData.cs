using LilFamStudioNet5.ViewModels.Visit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Models
{
    public class DanceGroupLessonUserVisitsData
    {
        public DanceGroupLesson danceGroupLesson { get; set; }
        public LinkedList<VisitLessonUserVisitStatus> visitLessonUserVisitStatuses { get; set; }

        public DanceGroupLessonUserVisitsData(DanceGroupLesson danceGroupLesson, LinkedList<VisitLessonUserVisitStatus> visitLessonUserVisitStatuses)
        {
            this.danceGroupLesson = danceGroupLesson;
            this.visitLessonUserVisitStatuses = visitLessonUserVisitStatuses;
        }
    }
}
