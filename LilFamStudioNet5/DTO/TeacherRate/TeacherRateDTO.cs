using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.TeacherRate
{
    public class TeacherRateDTO
    {
        [Required(ErrorMessage = "no_id_of_teacher_rate")]
        public int id_of_teacher_rate { get; set; }

        [Required(ErrorMessage = "no_name")]
        public string name { get; set; }

        [Required(ErrorMessage = "no_value")]
        public int value { get; set; }
    }
}
