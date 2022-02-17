using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Abonement
{
    public class AbonementsBySpecialStatusViewModel
    {
        public List<AbonementLiteViewModel> abonementLiteViewModelsRaz { get; set; }
        public List<AbonementLiteViewModel> abonementLiteViewModelsUsual { get; set; }
        public List<AbonementLiteViewModel> abonementLiteViewModelsUnlim { get; set; }
        public List<AbonementLiteViewModel> abonementLiteViewModelsRazTrial { get; set; }

        public AbonementsBySpecialStatusViewModel(List<AbonementLiteViewModel> abonementLiteViewModelsRaz, List<AbonementLiteViewModel> abonementLiteViewModelsUsual, List<AbonementLiteViewModel> abonementLiteViewModelsUnlim, List<AbonementLiteViewModel> abonementLiteViewModelsRazTrial)
        {
            this.abonementLiteViewModelsRaz = abonementLiteViewModelsRaz;
            this.abonementLiteViewModelsUsual = abonementLiteViewModelsUsual;
            this.abonementLiteViewModelsUnlim = abonementLiteViewModelsUnlim;
            this.abonementLiteViewModelsRazTrial = abonementLiteViewModelsRazTrial;
        }
    }
}
