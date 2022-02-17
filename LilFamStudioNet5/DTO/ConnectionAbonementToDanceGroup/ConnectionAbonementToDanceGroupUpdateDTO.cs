using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.ConnectionAbonementToDanceGroup
{
    public class ConnectionAbonementToDanceGroupUpdateDTO
    {
        [Required(ErrorMessage = "no_id_of_abonement")]
        public int id_of_abonement { get; set; }

        [Required(ErrorMessage = "no_id_of_dance_group")]
        public int id_of_dance_group { get; set; }

        public int status { get; set; }
    }
}
