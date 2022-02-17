using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("purchase_abonement")]
    public class PurchaseAbonement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public User user { get; set; }

        public Abonement abonement { get; set; }

        public DateTime? dateOfBuy { get; set; }
        public DateTime? dateOfAdd { get; set; }

        [Column("days", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int days { get; set; }

        [Column("visits", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int visits { get; set; }

        [Column("visits_left", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int visitsLeft { get; set; }

        [Column("price", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int price { get; set; }

        [Column("cashless", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int cashless { get; set; }

        public DateTime? dateOfActivation{ get; set; }
        public DateTime? dateOfMustBeUsedTo { get; set; }

        [Column("special_status", TypeName = "varchar(256)")]
        public string specialStatus { get; set; }

        [Column("comment", TypeName = "text")]
        public string comment { get; set; }

        [Column("active", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("1")]
        public int active { get; set; }
    }
}
