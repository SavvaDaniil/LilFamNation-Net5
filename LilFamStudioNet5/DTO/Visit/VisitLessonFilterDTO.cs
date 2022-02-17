﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Visit
{
    public class VisitLessonFilterDTO
    {
        [Required(ErrorMessage = "no_id_of_dance_group")]
        public int id_of_dance_group { get; set; }

        [Required(ErrorMessage = "no_filterDateFrom")]
        public string filterDateFrom { get; set; }

        [Required(ErrorMessage = "no_filterDateTo")]
        public string filterDateTo { get; set; }
    }
}
