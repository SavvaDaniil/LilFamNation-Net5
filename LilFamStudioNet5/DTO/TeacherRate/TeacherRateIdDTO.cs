using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.TeacherRate
{
    public class TeacherRateIdDTO
    {
        [Required(ErrorMessage = "no_id_of_teacher_rate")]
        public int id_of_teacher_rate { get; set; }
    }
}
