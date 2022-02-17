using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class PurchaseAbonementService
    {
        public ApplicationDbContext _dbc;
        public PurchaseAbonementService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<PurchaseAbonement> findById(int id)
        {
            return await _dbc.PurchasesAbonement
                .Include(p => p.abonement)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<PurchaseAbonement>> listAll()
        {
            return await _dbc.PurchasesAbonement.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<PurchaseAbonement>> listAllByDates(DateTime? dateFrom, DateTime? dateTo)
        {
            return await _dbc.PurchasesAbonement
                .Include(p => p.user)
                .Include(p => p.abonement)
                .Where(p => p.dateOfBuy >= dateFrom && p.dateOfBuy <= dateTo)
                .OrderBy(p => p.dateOfBuy).ToListAsync();
        }
        public async Task<List<PurchaseAbonement>> listAllByDates(DanceGroup danceGroup, DateTime? dateFrom, DateTime? dateTo)
        {
            return await _dbc.PurchasesAbonement
                .Where(p => p.dateOfBuy >= dateFrom && p.dateOfBuy <= dateTo)
                .OrderBy(p => p.dateOfBuy).ToListAsync();
        }

        public async Task<List<PurchaseAbonement>> listAllByUser(User user)
        {
            return await _dbc.PurchasesAbonement
                .Include(p => p.abonement)
                .Where(p => p.user == user)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<PurchaseAbonement>> listAllActiveByUser(User user)
        {
            return await _dbc.PurchasesAbonement
                .Include(p => p.abonement)
                .Where(p => p.user == user && p.visitsLeft > 0 && (p.dateOfMustBeUsedTo >= DateTime.Now || p.dateOfMustBeUsedTo == null))
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<PurchaseAbonement>> listAllActiveByUser(User user, DateTime? dateOfLesson)
        {
            return await _dbc.PurchasesAbonement
                .Include(p => p.abonement)
                .Where(p => p.user == user && p.visitsLeft > 0 && (p.dateOfMustBeUsedTo >= DateTime.Now || p.dateOfMustBeUsedTo == null) && p.dateOfBuy <= dateOfLesson)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<bool> addVisit(PurchaseAbonement purchaseAbonement)
        {
            if (purchaseAbonement.visitsLeft <= 0) return false;

            if(purchaseAbonement.dateOfActivation == null) if (!await activate(purchaseAbonement)) return false;

            purchaseAbonement.visitsLeft--;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<PurchaseAbonement> returnVisit(PurchaseAbonement purchaseAbonement)
        {
            purchaseAbonement.visitsLeft++;
            if (purchaseAbonement.visitsLeft == purchaseAbonement.visits && purchaseAbonement.dateOfActivation != null) if (!await deactivate(purchaseAbonement)) return null;
            await _dbc.SaveChangesAsync();
            return purchaseAbonement;
        }

        public async Task<bool> activate(PurchaseAbonement purchaseAbonement)
        {
            purchaseAbonement.dateOfActivation = DateTime.Now;
            purchaseAbonement.dateOfMustBeUsedTo = DateTime.Now.AddDays((purchaseAbonement.days <= 0 ? 1 : (purchaseAbonement.days - 1)));
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deactivate(PurchaseAbonement purchaseAbonement)
        {
            purchaseAbonement.dateOfActivation = null;
            purchaseAbonement.dateOfMustBeUsedTo = null;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> isTrialAlreadyBuyed(User user)
        {
            return await _dbc.PurchasesAbonement
                .Where(p => p.user == user && p.abonement.isTrial == 1)
                .AnyAsync();
        }


        public async Task<PurchaseAbonement> add(User user, Abonement abonement, int price, int cashless, int visits, int days, string comment, string dateOfBuyStr)
        {
            PurchaseAbonement purchaseAbonement = new PurchaseAbonement();
            purchaseAbonement.user = user;
            purchaseAbonement.abonement = abonement;
            purchaseAbonement.price = price;
            purchaseAbonement.cashless = cashless;
            purchaseAbonement.visits = visits;
            purchaseAbonement.visitsLeft = visits;
            purchaseAbonement.days = days;
            purchaseAbonement.comment = comment;
            purchaseAbonement.specialStatus = (abonement != null ? abonement.specialStatus : null);

            purchaseAbonement.dateOfAdd = DateTime.Now;

            DateTime dateOfBuy = DateTime.Now;
            if(dateOfBuyStr != null && dateOfBuyStr != "" && dateOfBuyStr != "null") DateTime.TryParse(dateOfBuyStr, out dateOfBuy);
            dateOfBuy = dateOfBuy.Date;

            purchaseAbonement.dateOfBuy = dateOfBuy;

            await _dbc.PurchasesAbonement.AddAsync(purchaseAbonement);
            await _dbc.SaveChangesAsync();

            return purchaseAbonement;
        }

        public async Task<bool> update(
            PurchaseAbonement purchaseAbonement, 
            Abonement abonement, 
            int price, 
            int cashless, 
            int days,
            int visits,
            int visitsLeft, 
            string comment, 
            DateTime? dateOfBuy,
            DateTime? dateOfActivation,
            DateTime? dateMustBeUsedTo
            )
        {
            purchaseAbonement.days = days;
            purchaseAbonement.price = price;
            purchaseAbonement.cashless = cashless;
            purchaseAbonement.visits = visits;
            purchaseAbonement.visitsLeft = visitsLeft;
            purchaseAbonement.comment = comment;
            purchaseAbonement.dateOfBuy = dateOfBuy;
            purchaseAbonement.dateOfActivation = dateOfActivation;
            purchaseAbonement.dateOfMustBeUsedTo = dateMustBeUsedTo;
            if (abonement != null) purchaseAbonement.abonement = abonement;
            await _dbc.SaveChangesAsync();
            return true;
        }




        public async Task<bool> delete(PurchaseAbonement purchaseAbonement)
        {
            _dbc.PurchasesAbonement.Remove(purchaseAbonement);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
