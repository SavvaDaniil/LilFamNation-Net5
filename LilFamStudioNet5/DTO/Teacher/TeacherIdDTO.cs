using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Teacher
{
    public class TeacherIdDTO
    {
        [Required(ErrorMessage = "no_id")]
        public int id_of_teacher { get; set; }
    }
}
