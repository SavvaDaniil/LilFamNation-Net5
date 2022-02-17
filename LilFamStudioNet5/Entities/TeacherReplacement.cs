using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("teacher_replacement")]
    public class TeacherReplacement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public DateTime? dateOfDay { get; set; }

        public DanceGroup danceGroup { get; set; }
        public DanceGroupDayOfWeek danceGroupDayOfWeek { get; set; }

        public Teacher teacherReplace { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }
    }
}
