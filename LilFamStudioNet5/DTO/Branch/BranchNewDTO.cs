using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Branch
{
    public class BranchNewDTO
    {
        [Required(ErrorMessage = "no_name")]
        public string name { get; set; }
    }
}
