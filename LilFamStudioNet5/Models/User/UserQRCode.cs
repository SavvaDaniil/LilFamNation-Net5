using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Models.User
{
    public class UserQRCode
    {
        public int id_of_user { get; set; }
        public string checkQR { get; set; }

        public UserQRCode(int id_of_user, string checkQR)
        {
            this.id_of_user = id_of_user;
            this.checkQR = checkQR;
        }
    }
}
