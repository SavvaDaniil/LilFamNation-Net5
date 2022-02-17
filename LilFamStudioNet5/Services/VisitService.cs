using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class VisitService
    {
        public ApplicationDbContext _dbc;
        public VisitService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<Visit> findById(int id)
        {
            return await _dbc.Visits
                .Include(p => p.user)
                .Include(p => p.danceGroup)
                .Include(p => p.danceGroupDayOfWeek)
                .Include(p => p.purchaseAbonement)
                .Include(p => p.purchaseAbonement.abonement)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<Visit> find(User user, DanceGroup danceGroup, DateTime dateOfBuy)
        {
            return await _dbc.Visits
                .Include(p => p.danceGroupDayOfWeek)
                .FirstOrDefaultAsync(p => p.user == user && p.danceGroup == danceGroup && p.dateOfBuy == dateOfBuy);
        }

        public async Task<List<Visit>> listByDanceGroupAndDateOfBuy(DanceGroup danceGroup, DateTime dateOfBuy)
        {
            return await _dbc.Visits
                .Include(p => p.user)
                .Include(p => p.purchaseAbonement)
                .Where(p => p.danceGroup == danceGroup && p.dateOfBuy == dateOfBuy)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAllByFilter(DanceGroup danceGroup, DateTime filterDateFrom, DateTime filterDateTo)
        {
            return await _dbc.Visits
                .Include(p => p.user)
                .Include(p => p.danceGroupDayOfWeek)
                .Include(p => p.purchaseAbonement)
                .Include(p => p.purchaseAbonement.abonement)
                .Where(p => p.danceGroup == danceGroup && p.dateOfBuy >= filterDateFrom && p.dateOfBuy <= filterDateTo)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAllByFilter(DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek, DateTime filterDateFrom, DateTime filterDateTo)
        {
            return await _dbc.Visits
                .Include(p => p.user)
                .Include(p => p.danceGroupDayOfWeek)
                .Include(p => p.purchaseAbonement)
                .Include(p => p.purchaseAbonement.abonement)
                .Where(p => p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek && p.dateOfBuy >= filterDateFrom && p.dateOfBuy <= filterDateTo)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAll(User user)
        {
            return await _dbc.Visits
                .Include(p => p.danceGroup)
                .Include(p => p.danceGroupDayOfWeek)
                .Include(p => p.purchaseAbonement)
                .Where(p => p.user == user)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAllByUserAndPurchaseAbonement(User user, PurchaseAbonement purchaseAbonement)
        {
            return await _dbc.Visits
                .Include(p => p.danceGroup)
                .Include(p => p.danceGroupDayOfWeek)
                .Where(p => p.user == user && p.purchaseAbonement == purchaseAbonement)
                .OrderByDescending(p => p.dateOfBuy)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAll(User user, DanceGroup danceGroup)
        {
            return await _dbc.Visits
                .Where(p => p.user == user && p.danceGroup == danceGroup)
                .OrderByDescending(p => p.id)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAll(User user, DanceGroup danceGroup, DateTime dateOfBuy)
        {
            return await _dbc.Visits
                .Include(p => p.danceGroup)
                .Include(p => p.danceGroupDayOfWeek)
                .Include(p => p.purchaseAbonement)
                .Where(p => p.user == user && p.danceGroup == danceGroup && p.dateOfBuy == dateOfBuy)
                .OrderByDescending(p => p.dateOfAdd)
                .ToListAsync();
        }

        public async Task<List<Visit>> listAll(User user, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek, DateTime dateOfBuy)
        {
            return await _dbc.Visits
                .Include(p => p.danceGroup)
                .Include(p => p.danceGroupDayOfWeek)
                .Include(p => p.purchaseAbonement)
                .Where(p => p.user == user && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek && p.dateOfBuy == dateOfBuy)
                .OrderByDescending(p => p.dateOfAdd)
                .ToListAsync();
        }

        public async Task<int> getNumberOfVisitByPurchaseAbonement(PurchaseAbonement purchaseAbonement, int id_of_visit)
        {
            return await _dbc.Visits
                .Where(p => p.id <= id_of_visit && p.purchaseAbonement == purchaseAbonement)
                .CountAsync();
        }
        public async Task<int> getNumberOfVisitByDanceGroupAndDataOfBuy(DanceGroup danceGroup, DateTime dateOfBuy)
        {
            return await _dbc.Visits
                .Where(p => p.danceGroup == danceGroup && p.dateOfBuy == dateOfBuy)
                .CountAsync();
        }


        public async Task<Visit> add(
            User user, 
            DanceGroup danceGroup, 
            DanceGroupDayOfWeek danceGroupDayOfWeek, 
            PurchaseAbonement purchaseAbonement, 
            DateTime dateOfBuy,
            bool isAddByApp = false,
            bool isReservation = false
        )
        {
            Visit visit = new Visit();
            visit.user = user;
            visit.danceGroup = danceGroup;
            visit.danceGroupDayOfWeek = danceGroupDayOfWeek;
            visit.purchaseAbonement = purchaseAbonement;

            visit.dateOfAdd = DateTime.Now;
            visit.dateOfBuy = dateOfBuy;
            visit.isAddByApp = (isAddByApp ? 1 : 0);
            if (isReservation) visit.isReservation = 1;

            await _dbc.Visits.AddAsync(visit);
            await _dbc.SaveChangesAsync();

            return visit;
        }

        public async Task<bool> delete(Visit visit)
        {
            _dbc.Remove(visit);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<int> countAllByPurchaseAbonement(PurchaseAbonement purchaseAbonement)
        {
            return await _dbc.Visits.Where(p => p.purchaseAbonement == purchaseAbonement).CountAsync();
        }

        public async Task<bool> reservationUpdate(Visit visit, int action)
        {
            visit.statusOfReservation = action;
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
