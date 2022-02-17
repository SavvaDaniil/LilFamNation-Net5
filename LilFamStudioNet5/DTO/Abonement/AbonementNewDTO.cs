using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Abonement
{
    public class AbonementNewDTO
    {
        [Required(ErrorMessage = "no_special_status")]
        public string special_status { get; set; }

        public int is_trial { get; set; }
    }
}
