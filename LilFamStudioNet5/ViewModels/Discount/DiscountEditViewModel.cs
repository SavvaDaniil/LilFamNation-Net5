using LilFamStudioNet5.ViewModels.Abonement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.Discount
{
    public class DiscountEditViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<AbonementWithConnectionToDiscountLiteViewModel> abonementWithConnectionToDiscountLiteViewModels { get; set; }

        public DiscountEditViewModel(int id, string name, List<AbonementWithConnectionToDiscountLiteViewModel> abonementWithConnectionToDiscountLiteViewModels)
        {
            this.id = id;
            this.name = name;
            this.abonementWithConnectionToDiscountLiteViewModels = abonementWithConnectionToDiscountLiteViewModels;
        }
    }
}
