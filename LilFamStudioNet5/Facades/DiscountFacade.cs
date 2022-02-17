using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Discount;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class DiscountFacade
    {
        public ApplicationDbContext _dbc;
        public DiscountFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<List<DiscountLiteViewModel>> listAllLite()
        {
            DiscountService discountService = new DiscountService(_dbc);
            List<Discount> discountes = await discountService.listAll();
            List<DiscountLiteViewModel> discountLiteViewModels = new List<DiscountLiteViewModel>();
            int i = 0;
            foreach (Discount discount in discountes)
            {
                i++;
                discountLiteViewModels.Add(new DiscountLiteViewModel(i, discount.id, discount.name, discount.dateOfAdd));
            }

            return discountLiteViewModels;
        }

        public async Task<List<DiscountWithConnectionToUserLiteViewModel>> listAllWithConnectectionToUserLite(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            DiscountService discountService = new DiscountService(_dbc);
            List<Discount> discountes = await discountService.listAll();
            List<DiscountWithConnectionToUserLiteViewModel> discountWithConnectionToUserLiteViewModels = new List<DiscountWithConnectionToUserLiteViewModel>();

            ConnectionDiscountToUserFacade connectionDiscountToUserFacade = new ConnectionDiscountToUserFacade(_dbc);
            List<int> listAllConnectedIdOfDiscountsByUser = await connectionDiscountToUserFacade.listAllConnectedIdOfDiscountsByUser(user);
            foreach (Discount discount in discountes)
            {
                discountWithConnectionToUserLiteViewModels.Add(
                    new DiscountWithConnectionToUserLiteViewModel(
                        discount.id,
                        discount.name,
                        (listAllConnectedIdOfDiscountsByUser.Contains(discount.id) ? 1 : 0)
                    )
                );
            }

            return discountWithConnectionToUserLiteViewModels;
        }


        public async Task<DiscountEditViewModel> getEdit(int id)
        {
            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(id);
            if (discount == null) return null;

            AbonementFacade abonementFacade = new AbonementFacade(_dbc);
            List<AbonementWithConnectionToDiscountLiteViewModel> abonementWithConnectionToDiscountLiteViewModels = await abonementFacade.listAllWithConnectionToDiscount(discount);


            DiscountEditViewModel discountEditViewModel = new DiscountEditViewModel(
                discount.id,
                discount.name,
                abonementWithConnectionToDiscountLiteViewModels
            );

            return discountEditViewModel;
        }

        public async Task<DiscountLiteViewModel> getLiteBydId(int id)
        {
            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(id);
            if (discount == null) return null;
            return new DiscountLiteViewModel(discount.id, discount.name, discount.dateOfAdd);
        }

        public async Task<Discount> add(DiscountNewDTO discountNewDTO)
        {
            DiscountService discountService = new DiscountService(_dbc);
            if (discountNewDTO.name == null) return null;
            Discount discount = await discountService.add(discountNewDTO.name);
            return discount;
        }

        public async Task<bool> delete(int id)
        {
            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(id);
            if (discount == null) return false;

            return await discountService.delete(discount);
        }

        public async Task<JsonAnswerStatus> update(DiscountEditByColumnDTO discountEditByColumnDTO)
        {
            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(discountEditByColumnDTO.id_of_discount);
            if (discount == null) return new JsonAnswerStatus("error", "not_found");

            await discountService.updateByColumn(discount, discountEditByColumnDTO.name, discountEditByColumnDTO.value);

            return new JsonAnswerStatus("success", null);
        }
    }
}
