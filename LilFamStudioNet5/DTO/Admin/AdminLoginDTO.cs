using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Admin
{
    public class AdminLoginDTO
    {
        [Required(ErrorMessage = "no_username")]
        public string username { get; set; }

        [Required(ErrorMessage = "no_password")]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
