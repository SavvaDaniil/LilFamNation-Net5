using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Admin
{
    public class AdminProfileDTO
    {
        [Required (ErrorMessage = "no_username")]
        public string username { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string password { get; set; }
    }
}
