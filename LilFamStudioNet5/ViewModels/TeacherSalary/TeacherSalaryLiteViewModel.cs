using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.TeacherSalary
{
    public class TeacherSalaryLiteViewModel
    {
        public int id { get; set; }
        public DateTime? dateOfUpdate { get; set; }

        public int id_of_teacher { get; set; }
        public string name_of_teacher { get; set; }

        public int id_of_dance_group { get; set; }
        public string name_of_dance_group { get; set; }

        public int status { get; set; }
        public int priceAuto { get; set; }
        public int isChangedByAdmin { get; set; }
        public int priceFact { get; set; }

        public TeacherSalaryLiteViewModel(int id, DateTime? dateOfUpdate, int id_of_teacher, string name_of_teacher, int id_of_dance_group, string name_of_dance_group, int status, int priceAuto, int isChangedByAdmin, int priceFact)
        {
            this.id = id;
            this.dateOfUpdate = dateOfUpdate;
            this.id_of_teacher = id_of_teacher;
            this.name_of_teacher = name_of_teacher;
            this.id_of_dance_group = id_of_dance_group;
            this.name_of_dance_group = name_of_dance_group;
            this.status = status;
            this.priceAuto = priceAuto;
            this.isChangedByAdmin = isChangedByAdmin;
            this.priceFact = priceFact;
        }
    }
}
