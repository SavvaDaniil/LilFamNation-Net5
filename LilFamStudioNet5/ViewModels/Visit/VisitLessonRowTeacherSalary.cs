using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Visit
{
    public class VisitLessonRowTeacherSalary
    {
        public List<VisitLessonTeacherSalary> visitLessonTeacherSalaries { get; set; }
        public int summAll { get; set; }

        public VisitLessonRowTeacherSalary()
        {
            this.visitLessonTeacherSalaries = new List<VisitLessonTeacherSalary>();
        }
    }
}
