using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("abonement")]
    public class Abonement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        [Column("name", TypeName = "varchar(256)")]
        public string name { get; set; }

        [Column("special_status", TypeName = "varchar(32)")]
        public string specialStatus { get; set; }

        [Column("days", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int days { get; set; }

        [Column("price", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int price { get; set; }

        [Column("visits", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int visits { get; set; }

        [Column("status_of_visible", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfVisible { get; set; }

        [Column("status_of_deleted", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfDeleted { get; set; }

        [Column("status_for_app", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfApp { get; set; }

        [Column("is_trial", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isTrial { get; set; }

        [Column("is_private", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isPrivate { get; set; }
    }
}
