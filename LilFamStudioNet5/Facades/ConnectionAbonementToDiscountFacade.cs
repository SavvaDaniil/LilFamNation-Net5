using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionAbonementToDiscount;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.ConnectionAbonementToDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class ConnectionAbonementToDiscountFacade
    {
        public ApplicationDbContext _dbc;
        public ConnectionAbonementToDiscountFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<int>> listAllConnectedIdOfAbonementsByIdOfDiscount(int id_of_discount)
        {
            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(id_of_discount);
            if (discount == null) return null;

            return await listAllConnectedIdOfAbonementsByDiscount(discount);
        }

        public async Task<List<int>> listAllConnectedIdOfAbonementsByDiscount(Discount discount)
        {
            ConnectionAbonementToDiscountService connectionAbonementToDiscountService = new ConnectionAbonementToDiscountService(_dbc);
            List<ConnectionAbonementToDiscount> connectionAbonementToDiscounts = await connectionAbonementToDiscountService.listAllByDiscount(discount);
            List<int> listAllConnectedIdOfAbonementsByDiscount = new List<int>();
            foreach (ConnectionAbonementToDiscount connectionAbonementToDiscount in connectionAbonementToDiscounts)
            {
                if (connectionAbonementToDiscount.abonement == null) continue;
                listAllConnectedIdOfAbonementsByDiscount.Add(connectionAbonementToDiscount.abonement.id);
            }

            return listAllConnectedIdOfAbonementsByDiscount;
        }


        public async Task<bool> update(ConnectionAbonementToDiscountUpdateDTO connectionAbonementToDiscountUpdateDTO)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(connectionAbonementToDiscountUpdateDTO.id_of_abonement);
            if (abonement == null) return false;

            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(connectionAbonementToDiscountUpdateDTO.id_of_discount);
            if (discount == null) return false;

            ConnectionAbonementToDiscountService connectionAbonementToDiscountService = new ConnectionAbonementToDiscountService(_dbc);
            if (connectionAbonementToDiscountUpdateDTO.name == "status" && connectionAbonementToDiscountUpdateDTO.value == 1)
            {
                ConnectionAbonementToDiscount connectionAbonementToDiscount = await connectionAbonementToDiscountService.add(abonement, discount);
                if (connectionAbonementToDiscount != null) return true;
            }
            else if (connectionAbonementToDiscountUpdateDTO.name == "status" && connectionAbonementToDiscountUpdateDTO.value == 0)
            {
                if (await connectionAbonementToDiscountService.delete(abonement, discount)) return true;
            } else if (connectionAbonementToDiscountUpdateDTO.name == "procent")
            {
                ConnectionAbonementToDiscount connectionAbonementToDiscount = await connectionAbonementToDiscountService.findByAbonementAndDiscount(abonement, discount);
                if (connectionAbonementToDiscount == null) return false;
                if (await connectionAbonementToDiscountService.update(connectionAbonementToDiscount, connectionAbonementToDiscountUpdateDTO.value)) return true;
            }

            return false;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionAbonementToDiscountService connectionAbonementToDiscountService = new ConnectionAbonementToDiscountService(_dbc);
            ConnectionAbonementToDiscount connectionAbonementToDiscount = await connectionAbonementToDiscountService.findById(id);
            if (connectionAbonementToDiscount == null) return false;

            return await connectionAbonementToDiscountService.delete(connectionAbonementToDiscount);
        }
    }
}
