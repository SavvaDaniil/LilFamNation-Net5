using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("teacher_rate")]
    public class TeacherRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        public Teacher teacher { get; set; }

        [Column("special", TypeName = "varchar(10)")]
        public string special { get; set; }

        [Column("students", TypeName = "int(11)")]
        public int students { get; set; }

        [Column("how_much_money", TypeName = "int(11)")]
        public int howMuchMoney { get; set; }
    }
}
