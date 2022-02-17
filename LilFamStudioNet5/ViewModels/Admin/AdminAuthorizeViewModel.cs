using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Admin
{
    public class AdminAuthorizeViewModel
    {
        public string menuActive { get; set; }
        public int level { get; set; }
        public List<string> listOfAvailablePanels { get; set; }

        public AdminAuthorizeViewModel(string menuActive, int level, List<string> listOfAvailablePanels)
        {
            this.menuActive = menuActive;
            this.level = level;
            this.listOfAvailablePanels = listOfAvailablePanels;
        }
    }
}
