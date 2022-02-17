using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.PurchaseAbonement;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.PurchaseAbonement;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class PurchaseAbonementFacade
    {
        public ApplicationDbContext _dbc;
        public PurchaseAbonementFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<JsonAnswerStatus> addByAdmin(PurchaseAbonementNewDTO purchaseAbonementNewDTO)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(purchaseAbonementNewDTO.id_of_user);
            if (user == null) return null;

            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(purchaseAbonementNewDTO.id_of_abonement);
            if (abonement == null) return null;

            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            PurchaseAbonement purchaseAbonement = await purchaseAbonementService.add(
                user,
                abonement,
                purchaseAbonementNewDTO.price,
                purchaseAbonementNewDTO.cashless,
                purchaseAbonementNewDTO.visits,
                purchaseAbonementNewDTO.days,
                purchaseAbonementNewDTO.comment,
                purchaseAbonementNewDTO.date_of_buy
            );

            if (purchaseAbonement == null) new JsonAnswerStatus("error", "unknown");

            if (purchaseAbonementNewDTO.id_of_dance_group != 0 && purchaseAbonementNewDTO.id_of_dance_group_day_of_week != 0)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                await visitFacade.addByAdmin(
                    user,
                    purchaseAbonementNewDTO.id_of_dance_group,
                    purchaseAbonementNewDTO.id_of_dance_group_day_of_week,
                    purchaseAbonement,
                    purchaseAbonementNewDTO.date_of_buy
                );
            }

            return new JsonAnswerStatus("success", null);
        }

        public async Task<JsonAnswerStatus> getForUserAdmin(HttpContext httpContext, int id_of_purchase_abonement)
        {
            UserFacade userFacade = new UserFacade(_dbc);
            User user = await userFacade.getCurrentOrNull(httpContext);
            if (user == null) return null;
            if (user.statusOfAdmin != 1) return new JsonAnswerStatus("error", "not_admin");

            PurchaseAbonementLiteViewModel purchaseAbonementLiteViewModel = await getLiteViewModel(id_of_purchase_abonement);
            if (purchaseAbonementLiteViewModel == null) return new JsonAnswerStatus("error", "not_found");
            return new JsonAnswerStatus("success", null, purchaseAbonementLiteViewModel);
        }

        public async Task<PurchaseAbonementLiteViewModel> getLiteViewModel(int id_of_purchase_abonement)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            PurchaseAbonement purchaseAbonement = await purchaseAbonementService.findById(id_of_purchase_abonement);
            if (purchaseAbonement == null) return null;
            return new PurchaseAbonementLiteViewModel(
                purchaseAbonement.id,
                (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : "Название утеряно"),
                purchaseAbonement.dateOfBuy,
                purchaseAbonement.dateOfActivation,
                purchaseAbonement.dateOfMustBeUsedTo,
                purchaseAbonement.days,
                purchaseAbonement.price,
                purchaseAbonement.cashless,
                purchaseAbonement.visits,
                purchaseAbonement.visitsLeft,
                purchaseAbonement.specialStatus,
                getIsExpired(purchaseAbonement)
            );
        }


        public async Task<List<PurchaseAbonementLiteViewModel>> listAllActiveByIdOfUser(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;
            return await listAllActiveOfUser(user);
        }

        public async Task<List<PurchaseAbonementLiteViewModel>> listAllActiveOfUser(User user, DanceGroup danceGroup = null, DateTime? dateOfBuyFrom = null)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            List<PurchaseAbonement> purchaseAbonements = (
                dateOfBuyFrom != null 
                ? await purchaseAbonementService.listAllActiveByUser(user, dateOfBuyFrom)
                : await purchaseAbonementService.listAllActiveByUser(user)
            );
            //System.Diagnostics.Debug.WriteLine("purchaseAbonements.count: " + purchaseAbonements.Count());

            HashSet<int> hashAllConnectedIdOfAbonementsToDanceGroup = new HashSet<int>();
            if (danceGroup != null)
            {
                if (danceGroup.isAbonementsAllowAll == 0)
                {
                    ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
                    if (await connectionUserToDanceGroupService.isAnyDanceGroupAndUser(user, danceGroup))
                    {
                        //список всех абонементов, что подключены к группе
                        ConnectionAbonementToDanceGroupFacade connectionAbonementToDanceGroupFacade = new ConnectionAbonementToDanceGroupFacade(_dbc);
                        hashAllConnectedIdOfAbonementsToDanceGroup = await connectionAbonementToDanceGroupFacade.hashAllConnectedIdOfAbonementsByDanceGroup(danceGroup);
                    }
                }
            }

            List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels = new List<PurchaseAbonementLiteViewModel>();
            foreach (PurchaseAbonement purchaseAbonement in purchaseAbonements)
            {
                if (purchaseAbonement.abonement != null && danceGroup != null)
                {
                    if (danceGroup.isAbonementsAllowAll == 0)
                    {
                        if (!hashAllConnectedIdOfAbonementsToDanceGroup.Contains(purchaseAbonement.abonement.id)) continue;
                    }
                }

                purchaseAbonementLiteViewModels.Add(
                    new PurchaseAbonementLiteViewModel(
                        purchaseAbonement.id,
                        (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : "Название утеряно"),
                        purchaseAbonement.dateOfBuy,
                        purchaseAbonement.dateOfActivation,
                        purchaseAbonement.dateOfMustBeUsedTo,
                        purchaseAbonement.days,
                        purchaseAbonement.price,
                        purchaseAbonement.cashless,
                        purchaseAbonement.visits,
                        purchaseAbonement.visitsLeft,
                        purchaseAbonement.specialStatus,
                        getIsExpired(purchaseAbonement)
                    )
                );
            }
            return purchaseAbonementLiteViewModels;
        }


        public async Task<List<PurchaseAbonementLiteViewModel>> listAllForUser(HttpContext httpContext, int id_of_dance_group = 0)
        {
            UserFacade userFacade = new UserFacade(_dbc);
            User user = await userFacade.getCurrentOrNull(httpContext);
            if (user == null) return null;

            DanceGroup danceGroup = null;
            if(id_of_dance_group != 0)
            {
                DanceGroupService danceGroupService = new DanceGroupService(_dbc);
                danceGroup = await danceGroupService.findById(id_of_dance_group);
            }

            return await listAllByUser(user, danceGroup);
        }

        public async Task<List<PurchaseAbonementLiteViewModel>> listAllByUser(User user, DanceGroup danceGroup = null)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            List<PurchaseAbonement> purchaseAbonements = await purchaseAbonementService.listAllByUser(user);

            //все подключенные приватные абонементы
            //ConnectionAbonementPrivateToUserFacade connectionAbonementPrivateToUserFacade = new ConnectionAbonementPrivateToUserFacade(_dbc);
            //HashSet<int> hashAllConnectedIdOfAbonementPrivatesByUser = await connectionAbonementPrivateToUserFacade.hashAllConnectedIdOfAbonementPrivatesByUser(user);

            HashSet<int> hashAllConnectedIdOfAbonementsToDanceGroup = new HashSet<int>();
            if (danceGroup != null)
            {
                if (danceGroup.isAbonementsAllowAll == 0)
                {
                    ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
                    if (await connectionUserToDanceGroupService.isAnyDanceGroupAndUser(user, danceGroup))
                    {
                        //список всех абонементов, что подключены к группе
                        ConnectionAbonementToDanceGroupFacade connectionAbonementToDanceGroupFacade = new ConnectionAbonementToDanceGroupFacade(_dbc);
                        hashAllConnectedIdOfAbonementsToDanceGroup = await connectionAbonementToDanceGroupFacade.hashAllConnectedIdOfAbonementsByDanceGroup(danceGroup);
                    }
                }
            }

            List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels = new List<PurchaseAbonementLiteViewModel>();
            foreach (PurchaseAbonement purchaseAbonement in purchaseAbonements)
            {
                if(purchaseAbonement.abonement != null && danceGroup != null)
                {
                    if(danceGroup.isAbonementsAllowAll == 0)
                    {
                        if (!hashAllConnectedIdOfAbonementsToDanceGroup.Contains(purchaseAbonement.abonement.id)) continue;
                    }
                }
                purchaseAbonementLiteViewModels.Add(
                    new PurchaseAbonementLiteViewModel(
                        purchaseAbonement.id,
                        (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : "Название утеряно"),
                        purchaseAbonement.dateOfBuy,
                        (purchaseAbonement.dateOfActivation != null ? purchaseAbonement.dateOfActivation.Value.Date : null),
                        (purchaseAbonement.dateOfMustBeUsedTo != null ? purchaseAbonement.dateOfMustBeUsedTo.Value.Date : null),
                        purchaseAbonement.days,
                        purchaseAbonement.price,
                        purchaseAbonement.cashless,
                        purchaseAbonement.visits,
                        purchaseAbonement.visitsLeft,
                        purchaseAbonement.specialStatus,
                        getIsExpired(purchaseAbonement)
                    )
                );
            }
            return purchaseAbonementLiteViewModels;
        }


        public async Task<List<PurchaseAbonementEditViewModel>> listAllEditByIdOfUser(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            List<PurchaseAbonement> purchaseAbonements = await purchaseAbonementService.listAllByUser(user);
            List<PurchaseAbonementEditViewModel> purchaseAbonementEditViewModels = new List<PurchaseAbonementEditViewModel>();

            VisitService visitService = new VisitService(_dbc);
            int visitsUsed = 0;
            foreach (PurchaseAbonement purchaseAbonement in purchaseAbonements)
            {
                visitsUsed = await visitService.countAllByPurchaseAbonement(purchaseAbonement);
                purchaseAbonementEditViewModels.Add(
                    new PurchaseAbonementEditViewModel(
                        purchaseAbonement.id,
                        purchaseAbonement.dateOfBuy,
                        (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.specialStatus : null),
                        (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.id : 0),
                        (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : null),
                        purchaseAbonement.days,
                        purchaseAbonement.dateOfActivation,
                        purchaseAbonement.dateOfMustBeUsedTo,
                        purchaseAbonement.visits,
                        purchaseAbonement.visitsLeft,
                        visitsUsed,
                        purchaseAbonement.price,
                        purchaseAbonement.cashless,
                        purchaseAbonement.comment
                    )    
                );
            }
            return purchaseAbonementEditViewModels;
        }

        public async Task<JsonAnswerStatus> getEdit(int id_of_purchase_abonement)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            PurchaseAbonement purchaseAbonement = await purchaseAbonementService.findById(id_of_purchase_abonement);
            if (purchaseAbonement == null) return new JsonAnswerStatus("error", "not_found");

            VisitService visitService = new VisitService(_dbc);
            int visitsUsed = await visitService.countAllByPurchaseAbonement(purchaseAbonement);

            AbonementFacade abonementFacade = new AbonementFacade(_dbc);
            List<AbonementLiteViewModel> abonementLiteViewModels = await abonementFacade.listAllLite();

            return new JsonAnswerStatus(
                "success", 
                null,
                abonementLiteViewModels,
                new PurchaseAbonementEditViewModel(
                    purchaseAbonement.id,
                    purchaseAbonement.dateOfBuy,
                    (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.specialStatus : null),
                    (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.id : 0),
                    (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : null),
                    purchaseAbonement.days,
                    purchaseAbonement.dateOfActivation,
                    purchaseAbonement.dateOfMustBeUsedTo,
                    purchaseAbonement.visits,
                    purchaseAbonement.visitsLeft,
                    visitsUsed,
                    purchaseAbonement.price,
                    purchaseAbonement.cashless,
                    purchaseAbonement.comment
                )
            );
        }

        public async Task<JsonAnswerStatus> update(PurchaseAbonementDTO purchaseAbonementDTO)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            PurchaseAbonement purchaseAbonement = await purchaseAbonementService.findById(purchaseAbonementDTO.id_of_purchase_abonement);
            if (purchaseAbonement == null) return new JsonAnswerStatus("error", "not_found");

            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(purchaseAbonementDTO.id_of_abonement);

            DateTime? dateOfBuyOpt = null;
            DateTime dateOfBuy;
            if(DateTime.TryParse(purchaseAbonementDTO.dateOfBuy, out dateOfBuy))
            {
                dateOfBuyOpt = dateOfBuy;
            }
            DateTime? dateOfActivationOpt = null;
            DateTime dateOfActivation;
            if (DateTime.TryParse(purchaseAbonementDTO.dateOfActivation, out dateOfActivation))
            {
                dateOfActivationOpt = dateOfActivation;
            }
            DateTime? dateOfMustBeUsedToOpt = null;
            DateTime dateOfMustBeUsedTo;
            if (DateTime.TryParse(purchaseAbonementDTO.dateMustBeUsedTo, out dateOfMustBeUsedTo))
            {
                dateOfMustBeUsedToOpt = dateOfMustBeUsedTo;
            }
            if (!await purchaseAbonementService.update(
                    purchaseAbonement,
                    abonement,
                    purchaseAbonementDTO.price,
                    purchaseAbonementDTO.cashless,
                    purchaseAbonementDTO.days,
                    purchaseAbonementDTO.visits,
                    purchaseAbonementDTO.visitsLeft,
                    purchaseAbonementDTO.comment,
                    dateOfBuyOpt,
                    dateOfActivationOpt,
                    dateOfMustBeUsedToOpt
                )
            ) return new JsonAnswerStatus("error", "unknown");

            return new JsonAnswerStatus("success", null);
        }


        public async Task<JsonAnswerStatus> listByDateOfBuyForAnalitycs(PurchaseAbonementAnalyticsDTO purchaseAbonementAnalyticsDTO)
        {
            DateTime dateOfBuy;
            if(!DateTime.TryParse(purchaseAbonementAnalyticsDTO.dateOfBuy, out dateOfBuy))return new JsonAnswerStatus("error", "no_data");

            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(purchaseAbonementAnalyticsDTO.id_of_dance_group);
            if (danceGroup == null) return new JsonAnswerStatus("error", "not_found");

            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            List<PurchaseAbonement> purchaseAbonements = await purchaseAbonementService.listAllByDates(dateOfBuy, dateOfBuy);
            List<PurchaseAbonementLiteViewModel> purchaseAbonementLiteViewModels = new List<PurchaseAbonementLiteViewModel>();

            int summAll = 0;

            purchaseAbonements.ForEach(purchaseAbonement =>
            {
                purchaseAbonementLiteViewModels.Add(
                    new PurchaseAbonementLiteViewModel(
                        purchaseAbonement.id,
                        (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : "Название утеряно"),
                        purchaseAbonement.dateOfBuy,
                        purchaseAbonement.dateOfActivation,
                        purchaseAbonement.dateOfMustBeUsedTo,
                        purchaseAbonement.days,
                        purchaseAbonement.price,
                        purchaseAbonement.cashless,
                        purchaseAbonement.visits,
                        purchaseAbonement.visitsLeft,
                        purchaseAbonement.specialStatus,
                        getIsExpired(purchaseAbonement),
                        (purchaseAbonement.user != null ? purchaseAbonement.user.id : 0),
                        (purchaseAbonement.user != null ? purchaseAbonement.user.fio : null)
                    )
                );
                summAll += purchaseAbonement.price + purchaseAbonement.cashless;
            });

            PurchaseAbonementStatistikLiteViewModel purchaseAbonementStatistikLiteViewModel = new PurchaseAbonementStatistikLiteViewModel(
                dateOfBuy,
                danceGroup.id,
                danceGroup.name,
                purchaseAbonementLiteViewModels,
                summAll
            );

            return new JsonAnswerStatus("success", null, purchaseAbonementStatistikLiteViewModel); ;
        }


        public bool getIsExpired(PurchaseAbonement purchaseAbonement)
        {
            if (purchaseAbonement.dateOfMustBeUsedTo == null) return false;
            return purchaseAbonement.dateOfMustBeUsedTo < DateTime.Now;
        }

        public PurchaseAbonementLiteViewModel getLiteViewModel(PurchaseAbonement purchaseAbonement)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            if (purchaseAbonement == null) return null;
            return new PurchaseAbonementLiteViewModel(
                purchaseAbonement.id,
                (purchaseAbonement.abonement != null ? purchaseAbonement.abonement.name : "Название утеряно"),
                (purchaseAbonement.dateOfBuy.HasValue ? purchaseAbonement.dateOfBuy.Value.Date : null),
                (purchaseAbonement.dateOfActivation.HasValue ? purchaseAbonement.dateOfActivation.Value.Date : null),
                (purchaseAbonement.dateOfMustBeUsedTo.HasValue ? purchaseAbonement.dateOfMustBeUsedTo.Value.Date : null),
                purchaseAbonement.days,
                purchaseAbonement.price,
                purchaseAbonement.cashless,
                purchaseAbonement.visits,
                purchaseAbonement.visitsLeft,
                purchaseAbonement.specialStatus,
                getIsExpired(purchaseAbonement),
                (purchaseAbonement.user != null ? purchaseAbonement.user.id : 0),
                (purchaseAbonement.user != null ? purchaseAbonement.user.fio : null)
            );
        }




        public async Task<bool> delete(int id_of_purchase_abonement)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            PurchaseAbonement purchaseAbonement = await purchaseAbonementService.findById(id_of_purchase_abonement);
            if (purchaseAbonement == null) return false;

            //тут нужно будет что-то сделать насчет слежки, при попытке удалить, ошибка произойдет
            return await purchaseAbonementService.delete(purchaseAbonement);
        }

        public async Task<bool> addVisit(PurchaseAbonement purchaseAbonement)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            return await purchaseAbonementService.addVisit(purchaseAbonement);
        }

        public async Task<bool> returnVisit(PurchaseAbonement purchaseAbonement, bool isReturnByAdmin = true)
        {
            PurchaseAbonementService purchaseAbonementService = new PurchaseAbonementService(_dbc);
            purchaseAbonement = await purchaseAbonementService.returnVisit(purchaseAbonement);
            if (purchaseAbonement == null) return false;

            if(isReturnByAdmin && purchaseAbonement.visitsLeft == purchaseAbonement.visits && purchaseAbonement.dateOfActivation == null && purchaseAbonement.abonement != null)
            {
                if (purchaseAbonement.abonement.specialStatus == "raz") await purchaseAbonementService.delete(purchaseAbonement);
            }
            return true;
        }

    }
}
