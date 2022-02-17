using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("dance_group")]
    public class DanceGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        [Column("name", TypeName = "varchar(256)")]
        public string name { get; set; }

        [Column("status", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int status { get; set; }

        public Teacher teacher { get; set; }
        public Branch branch { get; set; }

        [Column("description", TypeName = "text")]
        public string description { get; set; }

        [Column("status_of_creative", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfCreative { get; set; }

        [Column("status_of_app", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfApp { get; set; }

        [Column("is_abonements_allow_all", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isAbonementsAllowAll { get; set; }

        [Column("is_active_reservation", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isActiveReservation { get; set; }

        [Column("is_event", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isEvent { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }
    }
}
