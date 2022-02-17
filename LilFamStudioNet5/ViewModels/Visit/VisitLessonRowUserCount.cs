using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonRowUserCount
    {
        public List<VisitLessonUserCount> visitLessonUserCounts { get; set; }
        public int countAll { get; set; }

        public VisitLessonRowUserCount()
        {
            this.visitLessonUserCounts = new List<VisitLessonUserCount>();
        }
    }
}
