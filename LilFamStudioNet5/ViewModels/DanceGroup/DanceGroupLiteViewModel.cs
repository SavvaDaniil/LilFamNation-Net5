using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.DanceGroup
{
    public class DanceGroupLiteViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string teacherName { get; set; }
        public int status { get; set; }
        public int statusOfApp { get; set; }

        public DanceGroupLiteViewModel(int id, string name, string teacherName, int status, int statusOfApp)
        {
            this.id = id;
            this.name = name;
            this.teacherName = teacherName;
            this.status = status;
            this.statusOfApp = statusOfApp;
        }
    }
}
