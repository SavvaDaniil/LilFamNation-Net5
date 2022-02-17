using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Teacher
{
    public class TeacherLiteViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string posterSrc { get; set; }
        public bool isAnyDanceGroup { get; set; }

        public TeacherLiteViewModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public TeacherLiteViewModel(int id, string name, string posterSrc) : this(id, name)
        {
            this.posterSrc = posterSrc;
        }

        public TeacherLiteViewModel(int id, string name, string posterSrc, bool isAnyDanceGroup) : this(id, name)
        {
            this.posterSrc = posterSrc;
            this.isAnyDanceGroup = isAnyDanceGroup;
        }
    }
}
