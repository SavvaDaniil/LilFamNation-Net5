using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Abonement
{
    public class AbonementIdAndUserIdDTO
    {
        [Required(ErrorMessage = "no_id_of_user")]
        public int id_of_user { get; set; }

        [Required(ErrorMessage = "no_id_of_abonement")]
        public int id_of_abonement { get; set; }
    }
}
