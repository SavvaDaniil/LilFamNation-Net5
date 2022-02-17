using LilFamStudioNet5.ViewModels.TeacherRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Teacher
{
    public class TeacherEditViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string posterSrc { get; set; }
        public int stavka { get; set; }
        public int stavka_plus { get; set; }
        public List<TeacherRateLiteViewModel> teacherRateLiteViewModels { get; set; }
        public int min_students { get; set; }
        public int raz { get; set; }
        public int usual { get; set; }
        public int unlim { get; set; }
        public int procent { get; set; }
        public int plus_after_students { get; set; }
        public int plus_after_summa { get; set; }

        public TeacherEditViewModel(int id, string name, string posterSrc, int stavka, int stavka_plus, List<TeacherRateLiteViewModel> teacherRateLiteViewModels, int min_students, int raz, int usual, int unlim, int procent, int plus_after_students, int plus_after_summa)
        {
            this.id = id;
            this.name = name;
            this.posterSrc = posterSrc;
            this.stavka = stavka;
            this.stavka_plus = stavka_plus;
            this.teacherRateLiteViewModels = teacherRateLiteViewModels;
            this.min_students = min_students;
            this.raz = raz;
            this.usual = usual;
            this.unlim = unlim;
            this.procent = procent;
            this.plus_after_students = plus_after_students;
            this.plus_after_summa = plus_after_summa;
        }
    }
}
