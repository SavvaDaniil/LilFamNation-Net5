using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.ConnectionDiscountToUser
{
    public class ConnectionDiscountToUserUpdateDTO
    {
        [Required(ErrorMessage = "no_id_of_discount")]
        public int id_of_discount { get; set; }

        [Required(ErrorMessage = "no_id_of_user")]
        public int id_of_user { get; set; }

        public int status { get; set; }
    }
}
