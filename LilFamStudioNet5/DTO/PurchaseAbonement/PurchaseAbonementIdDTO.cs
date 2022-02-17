using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.PurchaseAbonement
{
    public class PurchaseAbonementIdDTO
    {
        [Required(ErrorMessage = "no_id_of_purchase_abonement")]
        public int id_of_purchase_abonement { get; set; }
    }
}
