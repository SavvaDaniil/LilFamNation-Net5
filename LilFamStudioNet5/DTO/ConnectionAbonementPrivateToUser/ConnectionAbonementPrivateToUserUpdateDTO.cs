using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.ConnectionAbonementPrivateToUser
{
    public class ConnectionAbonementPrivateToUserUpdateDTO
    {
        [Required(ErrorMessage = "no_id_of_abonement")]
        public int id_of_abonement { get; set; }

        [Required(ErrorMessage = "no_id_of_user")]
        public int id_of_user { get; set; }

        public int status { get; set; }
    }
}
