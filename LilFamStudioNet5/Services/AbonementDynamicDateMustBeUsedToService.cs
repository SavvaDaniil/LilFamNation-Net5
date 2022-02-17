using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class AbonementDynamicDateMustBeUsedToService
    {
        public ApplicationDbContext _dbc;
        public AbonementDynamicDateMustBeUsedToService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<AbonementDynamicDateMustBeUsedTo> findById(int id)
        {
            return await _dbc.AbonementDynamicDatesMustBeUsedTo.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<AbonementDynamicDateMustBeUsedTo>> listAll()
        {
            return await _dbc.AbonementDynamicDatesMustBeUsedTo.OrderByDescending(p => p.id).ToListAsync();
        }
        public async Task<List<AbonementDynamicDateMustBeUsedTo>> listAllByAbonement(Abonement abonement)
        {
            return await _dbc.AbonementDynamicDatesMustBeUsedTo
                .Where(p => p.abonement == abonement)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<AbonementDynamicDateMustBeUsedTo> add(Abonement abonement)
        {
            AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo = new AbonementDynamicDateMustBeUsedTo();
            abonementDynamicDateMustBeUsedTo.abonement = abonement;

            abonementDynamicDateMustBeUsedTo.dateFrom = DateTime.Now;
            abonementDynamicDateMustBeUsedTo.dateTo = DateTime.Now;
            abonementDynamicDateMustBeUsedTo.dateUsedTo = DateTime.Now;

            abonementDynamicDateMustBeUsedTo.dateOfAdd = DateTime.Now;
            abonementDynamicDateMustBeUsedTo.dateOfUpdate = DateTime.Now;

            await _dbc.AbonementDynamicDatesMustBeUsedTo.AddAsync(abonementDynamicDateMustBeUsedTo);
            await _dbc.SaveChangesAsync();

            return abonementDynamicDateMustBeUsedTo;
        }

        public async Task<bool> updateStatus(AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo, int statusNew)
        {
            if (statusNew != 0 && statusNew != 1) statusNew = 0;
            abonementDynamicDateMustBeUsedTo.status = statusNew;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateDate(AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo, string name, DateTime? dateTime)
        {
            switch (name)
            {
                case "dateFrom":
                    abonementDynamicDateMustBeUsedTo.dateFrom = dateTime;
                    break;
                case "dateTo":
                    abonementDynamicDateMustBeUsedTo.dateTo = dateTime;
                    break;
                case "dateUsedTo":
                    abonementDynamicDateMustBeUsedTo.dateUsedTo = dateTime;
                    break;
                default:
                    break;
            }
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo = await findById(id);
            if (abonementDynamicDateMustBeUsedTo == null) return false;
            _dbc.AbonementDynamicDatesMustBeUsedTo.Remove(abonementDynamicDateMustBeUsedTo);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(AbonementDynamicDateMustBeUsedTo abonementDynamicDateMustBeUsedTo)
        {
            _dbc.AbonementDynamicDatesMustBeUsedTo.Remove(abonementDynamicDateMustBeUsedTo);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
