using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.User
{
    public class UserProfileDTO
    {
        [Required(ErrorMessage = "no_username")]
        public string username { get; set; }

        public string fio { get; set; }
        public int sex { get; set; }
        public string phone { get; set; }
        public string passwordNew { get; set; }
    }
}
