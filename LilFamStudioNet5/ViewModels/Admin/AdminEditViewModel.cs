using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Admin
{
    public class AdminEditViewModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public int active { get; set; }
        public string position { get; set; }

        public List<AdminPanelAccessLiteViewModel> adminPanelAccessLiteViewModels { get; set; }

        public AdminEditViewModel(int id, string username, int active, string position, List<AdminPanelAccessLiteViewModel> adminPanelAccessLiteViewModels)
        {
            this.id = id;
            this.username = username;
            this.active = active;
            this.position = position;
            this.adminPanelAccessLiteViewModels = adminPanelAccessLiteViewModels;
        }
    }
}
