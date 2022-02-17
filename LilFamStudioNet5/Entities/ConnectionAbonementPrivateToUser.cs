using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("connection_abonement_private_to_user")]
    public class ConnectionAbonementPrivateToUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public Abonement abonement { get; set; }

        public User user { get; set; }

        public DateTime? dateOfAdd { get; set; }
    }
}
