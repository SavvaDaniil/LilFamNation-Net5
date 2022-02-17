using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("connection_abonement_to_discount")]
    public class ConnectionAbonementToDiscount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public Abonement abonement { get; set; }
        public Discount discount { get; set; }

        [Column("value", TypeName = "int(3)")]
        [System.ComponentModel.DefaultValue("0")]
        public int value { get; set; }

        public DateTime? dateOfAdd { get; set; }
    }
}
