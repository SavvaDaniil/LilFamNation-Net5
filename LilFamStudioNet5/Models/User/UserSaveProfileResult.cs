using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Models.User
{
    public class UserSaveProfileResult
    {
        public LilFamStudioNet5.Entities.User user { get; set; }
        public string status { get; set; }
        public string errors { get; set; }
        public bool isNeedRelogin { get; set; }

        public UserSaveProfileResult()
        {
        }

        public UserSaveProfileResult(Entities.User user)
        {
            this.user = user;
        }

        public UserSaveProfileResult(Entities.User user, bool isNeedRelogin)
        {
            this.user = user;
            this.isNeedRelogin = isNeedRelogin;
        }

        public UserSaveProfileResult(string status, string errors)
        {
            this.status = status;
            this.errors = errors;
        }
    }
}
