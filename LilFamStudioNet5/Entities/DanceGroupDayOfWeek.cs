using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("dance_group_day_of_week")]
    public class DanceGroupDayOfWeek
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public DanceGroup danceGroup { get; set; }

        [Column("day_of_week", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int dayOfWeek { get; set; }

        [Column("status", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int status { get; set; }

        public TimeSpan? timeFrom { get; set; }
        public TimeSpan? timeTo { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }
    }
}
