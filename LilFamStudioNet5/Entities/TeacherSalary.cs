using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("teacher_salary")]
    public class TeacherSalary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public DateTime? dateOfDay { get; set; }
        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }

        public DanceGroup danceGroup { get; set; }
        public DanceGroupDayOfWeek danceGroupDayOfWeek { get; set; }
        public Teacher teacher { get; set; }
        public Teacher teacherReplacement { get; set; }

        [Column("status", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int status { get; set; }

        [Column("price_auto", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int priceAuto { get; set; }

        [Column("is_changed_by_admin", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int isChangedByAdmin { get; set; }

        [Column("price_fact", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int priceFact { get; set; }
    }
}
