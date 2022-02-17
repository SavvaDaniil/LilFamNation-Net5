using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.User
{
    public class UserNewDTO
    {
        [Required(ErrorMessage = "no_fio")]
        public string fio { get; set; }

        public string phone { get; set; }
        public string comment { get; set; }
    }
}
