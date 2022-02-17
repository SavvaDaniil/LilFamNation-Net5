using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.PurchaseAbonement
{
    public class PurchaseAbonementDTO
    {
        [Required(ErrorMessage = "no_id_of_purchase_abonement")]
        public int id_of_purchase_abonement { get; set; }
        public int days { get; set; }
        public int id_of_abonement { get; set; }
        public int price { get; set; }
        public int cashless { get; set; }
        public int visits { get; set; }
        public int visitsLeft { get; set; }
        public string comment { get; set; }
        public string dateOfBuy { get; set; }
        public string dateOfActivation { get; set; }
        public string dateMustBeUsedTo { get; set; }
    }
}
