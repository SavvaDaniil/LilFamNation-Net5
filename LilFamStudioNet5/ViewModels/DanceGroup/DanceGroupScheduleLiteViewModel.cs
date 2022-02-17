using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupScheduleLiteViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string teacherName { get; set; }
        public int status { get; set; }
        public string timeFrom { get; set; }
        public string timeTo { get; set; }

        public DanceGroupScheduleLiteViewModel(int id, string name, string teacherName, int status, string timeFrom, string timeTo)
        {
            this.id = id;
            this.name = name;
            this.teacherName = teacherName;
            this.status = status;
            this.timeFrom = timeFrom;
            this.timeTo = timeTo;
        }
    }
}
