using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.Branch
{
    public class BranchEditByColumnDTO
    {
        [Required(ErrorMessage = "no_id_of_branch")]
        public int id_of_branch { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
