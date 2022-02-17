using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonsByDateViewModel
    {
        public LinkedList<VisitLessonHeaderDate> visitLessonHeaderDates { get; set; }
        public List<VisitLessonUserData> visitLessonUserDatas { get; set; }
        public VisitLessonRowPurchaseSumm visitLessonRowPurchaseSumm { get; set; }
        public VisitLessonRowUserCount visitLessonRowUserCount { get; set; }
        public VisitLessonRowTeacherSalary visitLessonRowTeacherSalary { get; set; }

        public VisitLessonsByDateViewModel(LinkedList<VisitLessonHeaderDate> visitLessonHeaderDates, List<VisitLessonUserData> visitLessonUserDatas, VisitLessonRowPurchaseSumm visitLessonRowPurchaseSumm, VisitLessonRowUserCount visitLessonRowUserCount, VisitLessonRowTeacherSalary visitLessonRowTeacherSalary)
        {
            this.visitLessonHeaderDates = visitLessonHeaderDates;
            this.visitLessonUserDatas = visitLessonUserDatas;
            this.visitLessonRowPurchaseSumm = visitLessonRowPurchaseSumm;
            this.visitLessonRowUserCount = visitLessonRowUserCount;
            this.visitLessonRowTeacherSalary = visitLessonRowTeacherSalary;
        }
    }
}
