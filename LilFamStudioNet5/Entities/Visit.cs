using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("visit")]
    public class Visit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public DateTime? dateOfBuy { get; set; }
        public DateTime? dateOfAdd { get; set; }

        public User user { get; set; }
        public DanceGroup danceGroup { get; set; }
        public DanceGroupDayOfWeek danceGroupDayOfWeek { get; set; }
        public PurchaseAbonement purchaseAbonement { get; set; }

        [Column("special_status_of_abonement", TypeName = "varchar(256)")]
        public string specialStatusOfAbonement { get; set; }

        [Column("is_add_by_app", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isAddByApp { get; set; }

        [Column("is_reservation", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isReservation { get; set; }

        [Column("status_of_reservation", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfReservation { get; set; }

        public DateTime? dateOfAcceptReservation { get; set; }
    }
}
