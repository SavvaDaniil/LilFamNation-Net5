using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonRowPurchaseSumm
    {
        public List<VisitLessonPurchaseSumm> visitLessonPurchaseSumms { get; set; }
        public int summFromAll { get; set; }

        public VisitLessonRowPurchaseSumm()
        {
            this.visitLessonPurchaseSumms = new List<VisitLessonPurchaseSumm>();
        }

        public VisitLessonRowPurchaseSumm(List<VisitLessonPurchaseSumm> visitLessonPurchaseSumms, int summFromAll)
        {
            this.visitLessonPurchaseSumms = visitLessonPurchaseSumms;
            this.summFromAll = summFromAll;
        }
    }
}
