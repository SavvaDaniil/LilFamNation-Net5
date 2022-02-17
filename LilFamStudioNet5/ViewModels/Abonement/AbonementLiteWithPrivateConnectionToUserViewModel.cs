using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Abonement
{
    public class AbonementLiteWithPrivateConnectionToUserViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int isPrivate { get; set; }
        public int status { get; set; }

        public AbonementLiteWithPrivateConnectionToUserViewModel(int id, string name, int isPrivate, int status)
        {
            this.id = id;
            this.name = name;
            this.isPrivate = isPrivate;
            this.status = status;
        }
    }
}
