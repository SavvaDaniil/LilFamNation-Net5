using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.DanceGroup
{
    public class DanceGroupNewDTO
    {
        [Required(ErrorMessage = "no_name")]
        public string name { get; set; }
    }
}
