﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.DanceGroupDayOfWeek
{
    public class DanceGroupDayOfWeekNewDTO
    {
        [Required(ErrorMessage = "no_id_of_dance_group")]
        public int id_of_dance_group { get; set; }
    }
}
