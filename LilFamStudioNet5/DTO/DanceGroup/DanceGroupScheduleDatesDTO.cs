using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.DanceGroup
{
    public class DanceGroupScheduleDatesDTO
    {
        [Required(ErrorMessage = "no_filterDateFromStr")]
        public string filterDateFromStr { get; set; }

        [Required(ErrorMessage = "no_filterDateToStr")]
        public string filterDateToStr { get; set; }
    }
}
