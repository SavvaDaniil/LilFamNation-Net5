using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Visit
{
    public class VisitIdDTO
    {
        [Required(ErrorMessage = "no_id_of_visit")]
        public int id_of_visit { get; set; }
    }
}
