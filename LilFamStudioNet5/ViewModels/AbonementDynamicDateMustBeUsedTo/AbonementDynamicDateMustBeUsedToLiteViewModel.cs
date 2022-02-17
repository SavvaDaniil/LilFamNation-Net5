using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.AbonementDynamicDateMustBeUsedTo
{
    public class AbonementDynamicDateMustBeUsedToLiteViewModel
    {
        public int id { get; set; }

        public int status { get; set; }

        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public DateTime? dateUsedTo { get; set; }

        public AbonementDynamicDateMustBeUsedToLiteViewModel(int id, int status, DateTime? dateFrom, DateTime? dateTo, DateTime? dateUsedTo)
        {
            this.id = id;
            this.status = status;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            this.dateUsedTo = dateUsedTo;
        }


        /*
        public int dateFromDay { get; set; }
        public int dateFromMonth { get; set; }
        public int dateFromYear { get; set; }

        public int dateToDay { get; set; }
        public int dateToMonth { get; set; }
        public int dateToYear { get; set; }

        public int dateUsedToDay { get; set; }
        public int dateUsedToMonth { get; set; }
        public int dateUsedToYear { get; set; }
        */

    }
}
