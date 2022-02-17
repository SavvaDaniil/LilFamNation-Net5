using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.AbonementDynamicDateMustBeUsedTo
{
    public class AbonementDynamicDateMustBeUsedToIdDTO
    {
        [Required(ErrorMessage = "no_id_of_abonement_dynamic_date_be_must_used_to")]
        public int id_of_abonement_dynamic_date_be_must_used_to { get; set; }
    }
}
