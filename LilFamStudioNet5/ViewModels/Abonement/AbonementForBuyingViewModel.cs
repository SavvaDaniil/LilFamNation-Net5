using LilFamStudioNet5.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Abonement
{
    public class AbonementForBuyingViewModel
    {
        public AbonementLiteViewModel abonementLiteViewModel { get; set; }
        public UserLiteViewModel userLiteViewModel { get; set; }

        public AbonementForBuyingViewModel(AbonementLiteViewModel abonementLiteViewModel, UserLiteViewModel userLiteViewModel)
        {
            this.abonementLiteViewModel = abonementLiteViewModel;
            this.userLiteViewModel = userLiteViewModel;
        }
    }
}
