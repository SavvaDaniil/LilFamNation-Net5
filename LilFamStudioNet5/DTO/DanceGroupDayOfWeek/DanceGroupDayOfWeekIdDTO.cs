using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.DanceGroupDayOfWeek
{
    public class DanceGroupDayOfWeekIdDTO
    {
        [Required(ErrorMessage = "no_id")]
        public int id_of_dance_group_day_of_week { get; set; }
    }
}
