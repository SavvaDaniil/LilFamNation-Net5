using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.TeacherSalary
{
    public class TeacherSalaryUpdatePriceDTO
    {
        [Required(ErrorMessage = "no_id_of_teacher_salary")]
        public int id_of_teacher_salary { get; set; }

        public int priceFact { get; set; }

        public int status { get; set; }
    }
}
