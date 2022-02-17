using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("branch")]
    public class Branch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }
        
        [Column("name", TypeName = "varchar(64)")]
        public string name { get; set; }

        [Column("description", TypeName = "text")]
        public string description { get; set; }

        [Column("coordinates", TypeName = "varchar(64)")]
        public string coordinates { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfUpdate { get; set; }
    }
}
