using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("user")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        [Column("username", TypeName = "varchar(256)")]
        public string username { get; set; }

        [Column("password", TypeName = "varchar(256)")]
        public string password { get; set; }

        [Column("auth_key", TypeName = "varchar(256)")]
        public string authKey { get; set; }

        [Column("access_token", TypeName = "varchar(256)")]
        public string accessToken { get; set; }

        [Column("active", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int active { get; set; }

        [Column("fio", TypeName = "varchar(256)")]
        public string fio { get; set; }

        [Column("phone", TypeName = "varchar(256)")]
        public string phone { get; set; }

        [Column("sex", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int sex { get; set; }

        [Column("parentFio", TypeName = "varchar(256)")]
        public string parentFio { get; set; }

        [Column("parentPhone", TypeName = "varchar(256)")]
        public string parentPhone { get; set; }

        [Column("comment", TypeName = "text")]
        public string comment { get; set; }

        public DateTime? dateOfBirthday { get; set; }

        [Column("statusOfTeacher", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfTeacher { get; set; }

        [Column("statusOfAdmin", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int statusOfAdmin { get; set; }

        [Column("checkQr", TypeName = "varchar(256)")]
        public string checkQr { get; set; }

        [Column("authAdmin0App1", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int authAdmin0App1 { get; set; }


        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfLastUpdateProfile { get; set; }
        public DateTime? dateOfLastVisit { get; set; }

        [Column("forget_count", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int forgetCount { get; set; }

        [Column("forget_code", TypeName = "varchar(6)")]
        public string forgetCode { get; set; }

        [Column("date_of_last_try")]
        public DateTime? forgetDateOfLastTry { get; set; }
    }
}