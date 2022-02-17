using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.DanceGroup
{
    public class DanceGroupIdDTO
    {
        [Required(ErrorMessage = "no_id_of_dance_group")]
        public int id_of_dance_group { get; set; }
    }
}
