using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.TeacherRate
{
    public class TeacherRateNewDTO
    {
        [Required(ErrorMessage = "no_id_of_teacher")]
        public int id_of_teacher { get; set; }
    }
}
