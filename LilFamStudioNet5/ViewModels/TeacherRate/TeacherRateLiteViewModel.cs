using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.TeacherRate
{
    public class TeacherRateLiteViewModel
    {
        public int id { get; set; }
        public string special { get; set; }
        public int students { get; set; }
        public int how_much_money { get; set; }

        public TeacherRateLiteViewModel(int id, string special, int students, int how_much_money)
        {
            this.id = id;
            this.special = special;
            this.students = students;
            this.how_much_money = how_much_money;
        }
    }
}
