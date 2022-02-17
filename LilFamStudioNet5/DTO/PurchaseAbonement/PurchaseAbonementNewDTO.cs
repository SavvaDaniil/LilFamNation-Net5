using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.PurchaseAbonement
{
    public class PurchaseAbonementNewDTO
    {
        [Required(ErrorMessage = "id_of_user")]
        public int id_of_user { get; set; }
        [Required(ErrorMessage = "no_id_of_abonement")]
        public int id_of_abonement { get; set; }
        public int price { get; set; }
        public int cashless { get; set; }
        public int visits { get; set; }
        public int days { get; set; }
        public string comment { get; set; }
        public string date_of_buy { get; set; }

        public int id_of_dance_group { get; set; }
        public int id_of_dance_group_day_of_week { get; set; }
    }
}
