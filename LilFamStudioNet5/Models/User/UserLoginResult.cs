using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Models.User
{
    public class UserLoginResult
    {
        public string status { get; set; }
        public string errors { get; set; }
        public Entities.User user { get; set; }
        public int id_of_user { get; set; }

        public UserLoginResult(string status, string errors)
        {
            this.status = status;
            this.errors = errors;
        }

        public UserLoginResult(string status, string errors, Entities.User user)
        {
            this.status = status;
            this.errors = errors;
            this.user = user;
        }

        public UserLoginResult(string status, string errors, int id_of_user) : this(status, errors)
        {
            this.id_of_user = id_of_user;
        }
    }
}
