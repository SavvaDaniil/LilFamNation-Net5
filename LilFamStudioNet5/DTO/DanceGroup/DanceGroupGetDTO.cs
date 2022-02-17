using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.DanceGroup
{
    public class DanceGroupGetDTO
    {
        [Required(ErrorMessage = "no_id_of_dance_group")]
        public int id_of_dance_group { get; set; }

        [Required(ErrorMessage = "no_id_of_dance_group_day_of_week")]
        public int id_of_dance_group_day_of_week {get;set;}

        [Required(ErrorMessage = "no_date_of_lesson_str")]
        public string date_of_lesson_str { get; set; }

        public int id_of_visit { get; set; }
    }
}
