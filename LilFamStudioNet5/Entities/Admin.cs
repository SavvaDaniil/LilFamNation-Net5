using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Entities
{
    [Table("admin")]
    public class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id", Order = 0)]
        public int id { get; set; }

        [Column("username", TypeName = "varchar(256)")]
        public string Username { get; set; }

        [Column("password", TypeName = "varchar(256)")]
        public string Password { get; set; }

        [Column("auth_key", TypeName = "varchar(256)")]
        public string AuthKey { get; set; }

        [Column("access_token", TypeName = "varchar(256)")]
        public string AccessToken { get; set; }

        [Column("active", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int active { get; set; }

        [Column("level", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int level { get; set; }

        [Column("position", TypeName = "varchar(256)")]
        public string position { get; set; }

        [Column("name", TypeName = "varchar(256)")]
        public string name { get; set; }

        public DateTime? dateOfAdd { get; set; }
        public DateTime? dateOfLastUpdateProfile { get; set; }



        [Column("panelAdmins", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelAdmins { get; set; }

        [Column("panelLessons", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelLessons{ get; set; }

        [Column("panelUsers", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelUsers { get; set; }

        [Column("panelDanceGroups", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelDanceGroups { get; set; }

        [Column("panelTeachers", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelTeachers { get; set; }

        [Column("panelAbonements", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelAbonements { get; set; }

        [Column("panelDiscounts", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelDiscounts { get; set; }

        [Column("panelBranches", TypeName = "int(1)")]
        [System.ComponentModel.DefaultValue("0")]
        public int panelBranches { get; set; }

    }
}
