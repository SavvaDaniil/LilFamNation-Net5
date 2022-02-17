using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.User
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "no_username")]
        public string username { get; set; }

        [Required(ErrorMessage = "no_password")]
        public string password { get; set; }

        [Required(ErrorMessage = "no_passwordAgain")]
        public string passwordAgain { get; set; }

        [Required(ErrorMessage = "no_fio")]
        public string fio { get; set; }

        [Required(ErrorMessage = "no_phone")]
        public string phone { get; set; }

        [Required(ErrorMessage = "no_sex")]
        public int sex { get; set; }

        [Required(ErrorMessage = "no_dateOfBirthdayStr")]
        public string dateOfBirthdayStr { get; set; }

        public string parentFio { get; set; }

        public string parentPhone { get; set; }
    }
}
