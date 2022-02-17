using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.DTO.User
{
    public class UserSearchDTO
    {
        public int page { get; set; }
        public string queryString { get; set; }
    }
}
