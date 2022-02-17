using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.ConnectionUserToDanceGroup
{
    public class ConnectionUserToDanceGroupUpdateDTO
    {
        [Required(ErrorMessage = "no_id_of_user")]
        public int id_of_user { get; set; }

        public int id_of_dance_group { get; set; }

        public int status { get; set; }
    }
}
