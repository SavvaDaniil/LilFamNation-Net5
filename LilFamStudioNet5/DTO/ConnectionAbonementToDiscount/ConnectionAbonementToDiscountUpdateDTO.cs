using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.ConnectionAbonementToDiscount
{
    public class ConnectionAbonementToDiscountUpdateDTO
    {
        [Required(ErrorMessage = "no_id_of_abonement")]
        public int id_of_abonement { get; set; }

        [Required(ErrorMessage = "no_id_of_discount")]
        public int id_of_discount { get; set; }

        public string name { get; set; }

        public int value { get; set; }
    }
}
