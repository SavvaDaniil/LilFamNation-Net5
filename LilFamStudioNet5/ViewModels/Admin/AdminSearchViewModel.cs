using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Admin
{
    public class AdminSearchViewModel
    {
        public List<AdminPreviewLiteViewModel> adminPreviewLiteViewModels { get; set; }
        public int searchCount { get; set; }

        public AdminSearchViewModel(List<AdminPreviewLiteViewModel> adminPreviewLiteViewModels, int searchCount)
        {
            this.adminPreviewLiteViewModels = adminPreviewLiteViewModels;
            this.searchCount = searchCount;
        }
    }
}
