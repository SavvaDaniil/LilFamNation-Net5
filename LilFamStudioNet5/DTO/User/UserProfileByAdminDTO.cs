using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.User
{
    public class UserProfileByAdminDTO
    {
        [Required(ErrorMessage = "no_id_of_user")]
        public int id_of_user { get; set; }

        public string fio { get; set; }
        public string phone { get; set; }
        public int sex { get; set; }
        public string username { get; set; }
        public string comment { get; set; }
        public string parentFio { get; set; }
        public string parentPhone { get; set; }
        public string passwordNew { get; set; }
    }
}
