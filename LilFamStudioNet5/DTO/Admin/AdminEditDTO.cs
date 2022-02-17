using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Admin
{
    public class AdminEditDTO
    {
        [Required(ErrorMessage = "no_id_of_admin")]
        public int id_of_admin { get; set; }
        public int active { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string position { get; set; }

        public string new_password { get; set; }

        
        public int panelAdmins { get; set; }
        public int panelLessons { get; set; }
        public int panelUsers { get; set; }
        public int panelDanceGroups { get; set; }
        public int panelTeachers { get; set; }
        public int panelAbonements { get; set; }
        public int panelDiscounts { get; set; }
        public int panelBranches { get; set; }
        
    }
}
