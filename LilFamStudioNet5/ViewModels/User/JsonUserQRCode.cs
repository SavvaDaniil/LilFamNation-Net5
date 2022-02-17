using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.User
{
    public class JsonUserQRCode
    {
        public string status { get; set; }
        public string errors { get; set; }
        public string qrCodeAsBase64 { get; set; }

        public JsonUserQRCode(string status, string errors)
        {
            this.status = status;
            this.errors = errors;
        }

        public JsonUserQRCode(string status, string errors, string qrCodeAsBase64) : this(status, errors)
        {
            this.qrCodeAsBase64 = qrCodeAsBase64;
        }
    }
}
