using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.AbonementDynamicDateMustBeUsedTo;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.AbonementDynamicDateMustBeUsedTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class AbonementDynamicDateMustBeUsedToFacade
    {
        public ApplicationDbContext _dbc;
        public AbonementDynamicDateMustBeUsedToFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<List<AbonementDynamicDateMustBeUsedToLiteViewModel>> listAllLiteByIdOfAbonement(int id_of_abonement)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(id_of_abonement);
            if (abonement == null) return null;
            return await listAllLiteByAbonement(abonement);
        }

        public async Task<List<AbonementDynamicDateMustBeUsedToLiteViewModel>> listAllLiteByAbonement(Abonement abonement)
        {
            AbonementDynamicDateMustBeUsedToService abonementDynamicDateMustBeUsedToService = new AbonementDynamicDateMustBeUsedToService(_dbc);
            List<AbonementDynamicDateMustBeUsedTo> abonementDynamicDatesMustBeUsedTo = await abonementDynamicDateMustBeUsedToService.listAllByAbonement(abonement);

            List<AbonementDynamicDateMustBeUsedToLiteViewModel> abonementDynamicDateMustBeUsedToLiteViewModels = new List<AbonementDynamicDateMustBeUsedToLiteViewModel>();

            foreach (AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo in abonementDynamicDatesMustBeUsedTo)
            {

                abonementDynamicDateMustBeUsedToLiteViewModels.Add(
                    new AbonementDynamicDateMustBeUsedToLiteViewModel(
                        abonementDynamicDateMustBeUsedTo.id,
                        abonementDynamicDateMustBeUsedTo.status,
                        (abonementDynamicDateMustBeUsedTo.dateFrom == null ? DateTime.Now.Date : abonementDynamicDateMustBeUsedTo.dateFrom),
                        (abonementDynamicDateMustBeUsedTo.dateTo == null ? DateTime.Now.Date : abonementDynamicDateMustBeUsedTo.dateTo),
                        (abonementDynamicDateMustBeUsedTo.dateUsedTo == null ? DateTime.Now.Date : abonementDynamicDateMustBeUsedTo.dateUsedTo)
                    )    
                );

                /*
                abonementDynamicDateMustBeUsedToLiteViewModels.Add(
                    new AbonementDynamicDateMustBeUsedToLiteViewModel(
                        abonementDynamicDateMustBeUsedTo.id,
                        abonementDynamicDateMustBeUsedTo.status,
                        (abonementDynamicDateMustBeUsedTo.dateFrom.HasValue ? abonementDynamicDateMustBeUsedTo.dateFrom.Value.Day : 0),
                        (abonementDynamicDateMustBeUsedTo.dateFrom.HasValue ? abonementDynamicDateMustBeUsedTo.dateFrom.Value.Month : 0),
                        (abonementDynamicDateMustBeUsedTo.dateFrom.HasValue ? abonementDynamicDateMustBeUsedTo.dateFrom.Value.Year : 0),

                        (abonementDynamicDateMustBeUsedTo.dateTo.HasValue ? abonementDynamicDateMustBeUsedTo.dateTo.Value.Day : 0),
                        (abonementDynamicDateMustBeUsedTo.dateTo.HasValue ? abonementDynamicDateMustBeUsedTo.dateTo.Value.Month : 0),
                        (abonementDynamicDateMustBeUsedTo.dateTo.HasValue ? abonementDynamicDateMustBeUsedTo.dateTo.Value.Year : 0),

                        (abonementDynamicDateMustBeUsedTo.dateUsedTo.HasValue ? abonementDynamicDateMustBeUsedTo.dateUsedTo.Value.Day : 0),
                        (abonementDynamicDateMustBeUsedTo.dateUsedTo.HasValue ? abonementDynamicDateMustBeUsedTo.dateUsedTo.Value.Month : 0),
                        (abonementDynamicDateMustBeUsedTo.dateUsedTo.HasValue ? abonementDynamicDateMustBeUsedTo.dateUsedTo.Value.Year : 0)
                    )
                );
                */
            }

            return abonementDynamicDateMustBeUsedToLiteViewModels;
        }


        public async Task<AbonementDynamicDateMustBeUsedTo> add(AbonementDynamicDateMustBeUsedToNewDTO abonementDynamicDateMustBeUsedToNewDTO)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(abonementDynamicDateMustBeUsedToNewDTO.id_of_abonement);
            if (abonement == null) return null;
            AbonementDynamicDateMustBeUsedToService abonementDynamicDateMustBeUsedToService = new AbonementDynamicDateMustBeUsedToService(_dbc);
            return await abonementDynamicDateMustBeUsedToService.add(abonement);
        }

        public async Task<bool> delete(int id)
        {
            AbonementDynamicDateMustBeUsedToService abonementDynamicDateMustBeUsedToService = new AbonementDynamicDateMustBeUsedToService(_dbc);
            AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo = await abonementDynamicDateMustBeUsedToService.findById(id);
            if (abonementDynamicDateMustBeUsedTo == null) return false;
            return await abonementDynamicDateMustBeUsedToService.delete(abonementDynamicDateMustBeUsedTo);
        }

        public async Task<JsonAnswerStatus> update(AbonementDynamicDateMustBeUsedToEditByColumnDTO abonementDynamicDateMustBeUsedToEditByColumnDTO)
        {
            AbonementDynamicDateMustBeUsedToService abonementDynamicDateMustBeUsedToService = new AbonementDynamicDateMustBeUsedToService(_dbc);
            AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo = 
                await abonementDynamicDateMustBeUsedToService.findById(abonementDynamicDateMustBeUsedToEditByColumnDTO.id_of_abonement_dynamic_date_be_must_used_to);
            if (abonementDynamicDateMustBeUsedTo == null) return new JsonAnswerStatus("error", "not_found");

            if (abonementDynamicDateMustBeUsedToEditByColumnDTO.name == "status")
            {
                if (await abonementDynamicDateMustBeUsedToService.updateStatus(abonementDynamicDateMustBeUsedTo, abonementDynamicDateMustBeUsedToEditByColumnDTO.value))
                    return new JsonAnswerStatus("success", null);
            } else if((abonementDynamicDateMustBeUsedToEditByColumnDTO.name == "dateFrom"
                || abonementDynamicDateMustBeUsedToEditByColumnDTO.name == "dateTo"
                || abonementDynamicDateMustBeUsedToEditByColumnDTO.name == "dateUsedTo"))
            {
                DateTime dateNew;
                if(DateTime.TryParse(abonementDynamicDateMustBeUsedToEditByColumnDTO.dateData, out dateNew))
                {
                    if (await abonementDynamicDateMustBeUsedToService.updateDate(
                        abonementDynamicDateMustBeUsedTo,
                        abonementDynamicDateMustBeUsedToEditByColumnDTO.name,
                        dateNew)
                    )
                        return new JsonAnswerStatus("success", null);
                } else
                {
                    if (await abonementDynamicDateMustBeUsedToService.updateDate(
                        abonementDynamicDateMustBeUsedTo,
                        abonementDynamicDateMustBeUsedToEditByColumnDTO.name,
                        DateTime.Now)
                    )
                        return new JsonAnswerStatus("success", null);
                }
            }

            return new JsonAnswerStatus("error", "unknown");
        }
    }
}
