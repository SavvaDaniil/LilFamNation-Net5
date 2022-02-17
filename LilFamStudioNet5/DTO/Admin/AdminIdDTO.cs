using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Admin
{
    public class AdminIdDTO
    {
        [Required(ErrorMessage = ("no_id_of_admin"))]
        public int id_of_admin { get; set; }
    }
}
