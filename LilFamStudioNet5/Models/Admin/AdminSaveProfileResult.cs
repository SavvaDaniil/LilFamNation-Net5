using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Models.Admin
{
    public class AdminSaveProfileResult
    {
        public LilFamStudioNet5.Entities.Admin admin { get; set; }
        public string status { get; set; }
        public string errors { get; set; }
        public bool isNeedRelogin { get; set; }

        public AdminSaveProfileResult(LilFamStudioNet5.Entities.Admin admin)
        {
            this.admin = admin;
            this.isNeedRelogin = false;
        }

        public AdminSaveProfileResult(string status, string errors)
        {
            this.status = status;
            this.errors = errors;
            this.isNeedRelogin = false;
        }
    }
}
