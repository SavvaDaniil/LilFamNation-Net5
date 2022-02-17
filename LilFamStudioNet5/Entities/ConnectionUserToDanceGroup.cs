using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("connection_user_to_dance_group")]
    public class ConnectionUserToDanceGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public User user { get; set; }
        public DanceGroup danceGroup { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }
    }
}
