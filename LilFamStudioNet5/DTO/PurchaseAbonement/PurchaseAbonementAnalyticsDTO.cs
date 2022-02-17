using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.PurchaseAbonement
{
    public class PurchaseAbonementAnalyticsDTO
    {
        [Required(ErrorMessage = "no_dateOfBuy")]
        public string dateOfBuy { get; set; }

        [Required(ErrorMessage = "no_id_of_dance_group")]
        public int id_of_dance_group { get; set; }
    }
}
