using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Discount
{
    public class DiscountEditByColumnDTO
    {
        [Required(ErrorMessage = "no_id_of_discount")]
        public int id_of_discount { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
