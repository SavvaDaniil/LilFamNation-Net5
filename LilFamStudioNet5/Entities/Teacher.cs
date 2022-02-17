using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("teacher")]
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        [Column("name", TypeName = "varchar(256)")]
        public string name { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfRefresh { get; set; }


        [Column("stavka", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int stavka { get; set; }

        [Column("min_students", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int minStudents { get; set; }

        [Column("raz", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int raz { get; set; }

        [Column("usual", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int usual { get; set; }

        [Column("unlim", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int unlim { get; set; }

        [Column("stavka_plus", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int stavkaPlus { get; set; }

        [Column("plus_after_students", TypeName = "int(5)")]
        [System.ComponentModel.DefaultValue("0")]
        public int plusAfterStudents { get; set; }

        [Column("plus_after_summa", TypeName = "int(11)")]
        [System.ComponentModel.DefaultValue("0")]
        public int plusAfterSumma { get; set; }

        [Column("procent", TypeName = "int(3)")]
        [System.ComponentModel.DefaultValue("0")]
        public int procent { get; set; }

    }
}
