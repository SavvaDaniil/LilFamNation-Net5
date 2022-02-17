using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Abonement;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.AbonementDynamicDateMustBeUsedTo;
using LilFamStudioNet5.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class AbonementFacade
    {
        public ApplicationDbContext _dbc;
        public AbonementFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<AbonementLiteViewModel>> listAllLite(bool isOrderByName = false)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            List<Abonement> abonements = (isOrderByName ? await abonementService.listAllNotDeletedOrderByName() : await abonementService.listAllNotDeleted());
            List<AbonementLiteViewModel> abonementLiteViewModels = new List<AbonementLiteViewModel>();
            int i = 0;
            foreach (Abonement abonement in abonements)
            {
                i++;
                abonementLiteViewModels.Add(
                    new AbonementLiteViewModel(
                        abonement.id,
                        abonement.name,
                        abonement.specialStatus,
                        abonement.days,
                        abonement.price,
                        abonement.visits,
                        abonement.statusOfVisible,
                        abonement.statusOfApp,
                        abonement.isPrivate,
                        abonement.isTrial
                    )
                );
            }

            return abonementLiteViewModels;
        }



        public async Task<List<AbonementLiteWithPrivateConnectionToUserViewModel>> listAllPrivateConnectionsToUser(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            AbonementService abonementService = new AbonementService(_dbc);
            List<Abonement> abonements = await abonementService.listAll();
            List<AbonementLiteWithPrivateConnectionToUserViewModel> abonementLiteWithPrivateConnectionToUserViewModels = new List<AbonementLiteWithPrivateConnectionToUserViewModel>();

            ConnectionAbonementPrivateToUserFacade connectionAbonementPrivateToUserFacade = new ConnectionAbonementPrivateToUserFacade(_dbc);
            List<int> listAllConnectedIdOfAbonementPrivatesByUser = await connectionAbonementPrivateToUserFacade.listAllConnectedIdOfAbonementPrivatesByUser(user);

            foreach (Abonement abonement in abonements)
            {
                abonementLiteWithPrivateConnectionToUserViewModels.Add(
                    new AbonementLiteWithPrivateConnectionToUserViewModel(
                        abonement.id,
                        abonement.name,
                        abonement.isPrivate,
                        (listAllConnectedIdOfAbonementPrivatesByUser.Contains(abonement.id) ? 1 : 0)
                    )
                );
            }

            return abonementLiteWithPrivateConnectionToUserViewModels;
        }



        private AbonementLiteViewModel getLiteFromEntity(Abonement abonement)
        {
            return new AbonementLiteViewModel(
                abonement.id,
                abonement.name,
                abonement.specialStatus,
                abonement.days,
                abonement.price,
                abonement.visits,
                abonement.statusOfVisible,
                abonement.statusOfApp,
                abonement.isPrivate,
                abonement.isTrial
            );
        }

        public async Task<AbonementsBySpecialStatusViewModel> listsAllBySpecialStatusForUserByAdmin(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            return await listsAllBySpecialStatus(false, user);
        }

        public async Task<AbonementsBySpecialStatusViewModel> appListsAllBySpecialStatusForUser(HttpContext httpContext)
        {
            UserFacade userFacade = new UserFacade(_dbc);
            User user = await userFacade.getCurrentOrNull(httpContext);
            if (user == null) return null;

            return await listsAllBySpecialStatus(true, user);
        }

        public async Task<AbonementsBySpecialStatusViewModel> listsAllBySpecialStatus(bool isForApp = false, User user = null, DanceGroup danceGroup = null)
        {
            HashSet<int> hashAllConnectedIdOfAbonementPrivatesByUser = new HashSet<int>();
            if (user != null)
            {
                ConnectionAbonementPrivateToUserFacade connectionAbonementPrivateToUserFacade = new ConnectionAbonementPrivateToUserFacade(_dbc);
                hashAllConnectedIdOfAbonementPrivatesByUser = await connectionAbonementPrivateToUserFacade.hashAllConnectedIdOfAbonementPrivatesByUser(user);
            }

            HashSet<int> hashAllConnectedIdOfAbonementsToDanceGroupOrAnyDanceGroupsByUser = new HashSet<int>();
            ConnectionAbonementToDanceGroupFacade connectionAbonementToDanceGroupFacade = new ConnectionAbonementToDanceGroupFacade(_dbc);
            if (danceGroup != null)
            {
                if (danceGroup.isAbonementsAllowAll == 0)
                {
                    ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
                    if (await connectionUserToDanceGroupService.isAnyDanceGroupAndUser(user, danceGroup))
                    {
                        //список всех абонементов, что подключены к группе
                        hashAllConnectedIdOfAbonementsToDanceGroupOrAnyDanceGroupsByUser = await connectionAbonementToDanceGroupFacade.hashAllConnectedIdOfAbonementsByDanceGroup(danceGroup);
                    }
                }
            } else
            {
                if (user != null)
                {
                    hashAllConnectedIdOfAbonementsToDanceGroupOrAnyDanceGroupsByUser = await connectionAbonementToDanceGroupFacade.hashAnyConnectedIdOfAbonementToAnyDanceGroup(user);
                    System.Diagnostics.Debug.WriteLine("Количество подключенных абонементов: " + hashAllConnectedIdOfAbonementsToDanceGroupOrAnyDanceGroupsByUser.Count());
                }
            }

            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            bool isTrialAlreadyBuyed = false;
            if (user != null)
            {
                isTrialAlreadyBuyed = await purchaseAbonementService.isTrialAlreadyBuyed(user);
            }

            AbonementService abonementService = new AbonementService(_dbc);
            List<Abonement> abonements = await abonementService.listAllNotDeleted();
            List<AbonementLiteViewModel> abonementLiteViewModelsRaz = new List<AbonementLiteViewModel>();
            List<AbonementLiteViewModel> abonementLiteViewModelsUsual = new List<AbonementLiteViewModel>();
            List<AbonementLiteViewModel> abonementLiteViewModelsUnlim = new List<AbonementLiteViewModel>();
            List<AbonementLiteViewModel> abonementLiteViewModelsRazTrial = new List<AbonementLiteViewModel>();

            foreach (Abonement abonement in abonements)
            {
                if (!isForApp && abonement.statusOfVisible != 1) continue;
                if (isForApp && abonement.statusOfApp != 1) continue;
                if (abonement.isTrial == 1 && isTrialAlreadyBuyed) continue;

                if (!hashAllConnectedIdOfAbonementsToDanceGroupOrAnyDanceGroupsByUser.Contains(abonement.id)) continue;
                if (abonement.isPrivate == 1) if (!hashAllConnectedIdOfAbonementPrivatesByUser.Contains(abonement.id)) continue;

                /*
                    {
                    if (!hashAllConnectedIdOfAbonementPrivatesByUser.Contains(abonement.id))
                    {
                        if (danceGroup != null)
                        {
                            if (danceGroup.isAbonementsAllowAll == 0)
                            {
                                if (!hashAllConnectedIdOfAbonementsToDanceGroup.Contains(abonement.id)) continue;
                            }
                        } else
                        {
                            continue;
                        }
                    }
                }
                */
                /*
                if (abonement.isPrivate == 1)
                {
                    if (!hashAllConnectedIdOfAbonementPrivatesByUser.Contains(abonement.id))
                    {
                        if (danceGroup != null)
                        {
                            if (danceGroup.isAbonementsAllowAll == 0)
                            {
                                if (!hashAllConnectedIdOfAbonementsToDanceGroup.Contains(abonement.id)) continue;
                            }
                        }
                    }
                }
                */

                if (abonement.specialStatus == "raz" && abonement.isTrial == 0) 
                    abonementLiteViewModelsRaz.Add(getLiteFromEntity(abonement));
                if (abonement.specialStatus == "usual" && abonement.isTrial == 0)
                    abonementLiteViewModelsUsual.Add(getLiteFromEntity(abonement));
                if (abonement.specialStatus == "unlim" && abonement.isTrial == 0)
                    abonementLiteViewModelsUnlim.Add(getLiteFromEntity(abonement));
                if (abonement.specialStatus == "raz" && abonement.isTrial == 1)
                    abonementLiteViewModelsRazTrial.Add(getLiteFromEntity(abonement));
            }
            return new AbonementsBySpecialStatusViewModel(
                abonementLiteViewModelsRaz,
                abonementLiteViewModelsUsual,
                abonementLiteViewModelsUnlim,
                abonementLiteViewModelsRazTrial
            );
        }

        /*
        public async Task<AbonementsBySpecialStatusViewModel> listsAllBySpecialStatusForUser(HttpContext httpContext)
        {
            UserFacade userFacade = new UserFacade(_dbc);
            User user = await userFacade.getCurrentOrNull(httpContext);
            if (user == null) return null;

            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            bool isTrialAlreadyBuyed = await purchaseAbonementService.isTrialAlreadyBuyed(user);
            ConnectionAbonementPrivateToUserFacade connectionAbonementPrivateToUserFacade = new ConnectionAbonementPrivateToUserFacade(_dbc);
            List<int> listAllConnectedIdOfAbonementPrivatesByUser = await connectionAbonementPrivateToUserFacade.listAllConnectedIdOfAbonementPrivatesByUser(user);

            ConnectionUserToDanceGroupFacade connectionUserToDanceGroupFacade = new ConnectionUserToDanceGroupFacade(_dbc);
            List<DanceGroup> danceGroupsConnectedToUser = await connectionUserToDanceGroupFacade.listAllDanceGroupsConnectedToUser(user);

            ConnectionAbonementToDanceGroupFacade connectionAbonementToDanceGroupFacade = new ConnectionAbonementToDanceGroupFacade(_dbc);

            AbonementService abonementService = new AbonementService(_dbc);
            List<Abonement> abonements = await abonementService.listAllNotDeleted();
            List<AbonementLiteViewModel> abonementLiteViewModelsRaz = new List<AbonementLiteViewModel>();
            List<AbonementLiteViewModel> abonementLiteViewModelsUsual = new List<AbonementLiteViewModel>();
            List<AbonementLiteViewModel> abonementLiteViewModelsUnlim = new List<AbonementLiteViewModel>();
            List<AbonementLiteViewModel> abonementLiteViewModelsRazTrial = new List<AbonementLiteViewModel>();

            //
            List<int> listAllIdsOfAbonementConnectedToDanceGroupThatConnectedToUser = new List<int>();


            foreach (Abonement abonement in abonements)
            {
                if (abonement.specialStatus == "raz" && abonement.isTrial == 0 && !isTrialAlreadyBuyed)abonementLiteViewModelsRaz.Add(getLiteFromEntity(abonement));
                
                //проверяем на приватный доступ
                if(abonement.isPrivate == 1)
                {
                    if (!listAllConnectedIdOfAbonementPrivatesByUser.Contains(abonement.id)) continue;
                    foreach (DanceGroup danceGroupConnectedToUser in danceGroupsConnectedToUser)
                    {
                        listAllIdsOfAbonementConnectedToDanceGroupThatConnectedToUser = 
                            await connectionAbonementToDanceGroupFacade.listAllConnectedIdOfAbonementsByDanceGroup(danceGroupConnectedToUser);
                    }
                    if (!listAllIdsOfAbonementConnectedToDanceGroupThatConnectedToUser.Contains(abonement.id)) continue;
                }

                if (abonement.specialStatus == "usual" && abonement.isTrial == 0)
                    abonementLiteViewModelsUsual.Add(getLiteFromEntity(abonement));
                if (abonement.specialStatus == "unlim" && abonement.isTrial == 0)
                    abonementLiteViewModelsUnlim.Add(getLiteFromEntity(abonement));
                if (abonement.specialStatus == "raz" && abonement.isTrial == 1)
                    abonementLiteViewModelsRazTrial.Add(getLiteFromEntity(abonement));
            }
            return new AbonementsBySpecialStatusViewModel(
                abonementLiteViewModelsRaz,
                abonementLiteViewModelsUsual,
                abonementLiteViewModelsUnlim,
                abonementLiteViewModelsRazTrial
            );
        }
        */


        public async Task<AbonementForBuyingViewModel> getForBuy(int id_of_user, int id_of_abonement)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(id_of_abonement);
            if (abonement == null) return null;

            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;
            return new AbonementForBuyingViewModel(
                getLiteFromEntity(abonement),
                new UserLiteViewModel(user.id, user.fio)
            );
        }

        public async Task<List<AbonementWithConnectionToDiscountLiteViewModel>> listAllWithConnectionToDiscount(Discount discount)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            List<Abonement> abonements = await abonementService.listAllNotDeleted();

            ConnectionAbonementToDiscountService connectionAbonementToDiscountService = new ConnectionAbonementToDiscountService(_dbc);
            List<ConnectionAbonementToDiscount> connectionAbonementToDiscounts = await connectionAbonementToDiscountService.listAllByDiscount(discount);

            List<AbonementWithConnectionToDiscountLiteViewModel> abonementWithConnectionToDiscountLiteViewModels = new List<AbonementWithConnectionToDiscountLiteViewModel>();

            ConnectionAbonementToDiscount connectedAbonementToDiscount = null;
            foreach (Abonement abonement in abonements)
            {
                connectedAbonementToDiscount = null;
                foreach (ConnectionAbonementToDiscount connectionAbonementToDiscount in connectionAbonementToDiscounts)
                {
                    if(connectionAbonementToDiscount.abonement == abonement)
                    {
                        connectedAbonementToDiscount = connectionAbonementToDiscount;
                        break;
                    }
                }

                abonementWithConnectionToDiscountLiteViewModels.Add(
                    new AbonementWithConnectionToDiscountLiteViewModel(
                        abonement.id,
                        discount.id,
                        abonement.name,
                        (connectedAbonementToDiscount != null ? 1 : 0),
                        (connectedAbonementToDiscount != null ? connectedAbonementToDiscount.value : 0),
                        abonement.price,
                        (connectedAbonementToDiscount != null ? (abonement.price - abonement.price * connectedAbonementToDiscount.value / 100) : abonement.price)
                    )    
                );
            }

            return abonementWithConnectionToDiscountLiteViewModels;
        }


        public async Task<AbonementEditViewModel> getEdit(int id)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(id);
            if (abonement == null) return null;

            AbonementDynamicDateMustBeUsedToFacade abonementDynamicDateMustBeUsedToFacade = new AbonementDynamicDateMustBeUsedToFacade(_dbc);
            List<AbonementDynamicDateMustBeUsedToLiteViewModel> abonementDynamicDateMustBeUsedToLiteViewModels = await abonementDynamicDateMustBeUsedToFacade.listAllLiteByAbonement(abonement);

            AbonementEditViewModel abonementEditViewModel = new AbonementEditViewModel(
                abonement.id,
                abonement.name,
                abonement.specialStatus,
                abonement.days,
                abonement.price,
                abonement.visits,
                abonement.statusOfVisible,
                abonement.statusOfDeleted,
                abonement.statusOfApp,
                abonement.isTrial,
                abonement.isPrivate,
                abonementDynamicDateMustBeUsedToLiteViewModels
            );

            return abonementEditViewModel;
        }

        public async Task<Abonement> add(AbonementNewDTO abonementNewDTO)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            if (abonementNewDTO.special_status == null) return null;
            Abonement abonement = await abonementService.add(abonementNewDTO.special_status, abonementNewDTO.is_trial);
            return abonement;
        }

        public async Task<bool> delete(int id)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(id);
            if (abonement == null) return false;

            //нужно сделать проверку, есть ли в базе покупки или какие-либо действия с абонементом, если есть, то фейковый статус удаления


            return await abonementService.delete(abonement);
        }

        public async Task<JsonAnswerStatus> update(AbonementEditByColumnDTO abonementEditByColumnDTO)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(abonementEditByColumnDTO.id_of_abonement);
            if (abonement == null) return new JsonAnswerStatus("error", "not_found");

            if(!await abonementService.updateByColumn(abonement, abonementEditByColumnDTO.name, abonementEditByColumnDTO.value)) return new JsonAnswerStatus("error", "unknown");

            return new JsonAnswerStatus("success", null);
        }
    }
}
