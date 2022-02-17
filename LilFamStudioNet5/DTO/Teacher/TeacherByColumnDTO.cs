using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Teacher
{
    public class TeacherByColumnDTO
    {
        [Required(ErrorMessage = "no_id_of_teacher")]
        public int id_of_teacher { get; set; }

        public string name { get; set; }

        public string value { get; set; }

        public IFormFile file { get; set; }
    }
}
