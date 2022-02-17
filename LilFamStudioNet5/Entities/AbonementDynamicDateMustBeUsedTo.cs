using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("abonement_dynamic_date_must_be_used_to")]
    public class AbonementDynamicDateMustBeUsedTo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public Abonement abonement { get; set; }

        [Column("status", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int status { get; set; }

        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public DateTime? dateUsedTo { get; set; }
        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }

    }
}
