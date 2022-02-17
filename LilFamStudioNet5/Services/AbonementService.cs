using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class AbonementService
    {
        public ApplicationDbContext _dbc;
        public AbonementService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<Abonement> findById(int id)
        {
            return await _dbc.Abonements.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<Abonement>> listAll()
        {
            return await _dbc.Abonements.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<Abonement>> listAllNotDeleted()
        {
            return await _dbc.Abonements
                .Where(p => p.statusOfDeleted == 0)
                .OrderByDescending(p => p.id)
                .ToListAsync();
        }

        public async Task<List<Abonement>> listAllNotDeletedOrderByName()
        {
            return await _dbc.Abonements
                .Where(p => p.statusOfDeleted == 0)
                .OrderBy(p => p.name)
                .ToListAsync();
        }

        public async Task<List<Abonement>> listAllNotDeletedBySpecialStatusOrderByName(string special_status, int is_trial)
        {
            return await _dbc.Abonements
                .Where(p => p.statusOfDeleted == 0 && p.specialStatus == special_status && p.isTrial == is_trial)
                .OrderBy(p => p.name)
                .ToListAsync();
        }


        public async Task<Abonement> add(string special_status, int is_trial)
        {
            if (special_status != "raz" && special_status != "usual" && special_status != "unlim") return null;
            Abonement abonement = new Abonement();
            abonement.name = "Default";
            abonement.specialStatus = special_status;
            abonement.isTrial = (is_trial == 1 ? 1 : 0);
            if (special_status == "raz")
            {
                abonement.visits = 1;
                abonement.days = 1;
            }

            await _dbc.Abonements.AddAsync(abonement);
            await _dbc.SaveChangesAsync();

            return abonement;
        }

        public async Task<bool> updateByColumn(Abonement abonement, string name, string value)
        {
            switch (name)
            {
                case "name":
                    abonement.name = value;
                    break;
                case "days":
                    abonement.days = int.Parse(value);
                    break;
                case "price":
                    abonement.price = int.Parse(value);
                    break;
                case "visits":
                    abonement.visits = int.Parse(value);
                    break;
                case "is_private":
                    abonement.isPrivate = int.Parse(value);
                    break;
                case "is_trial":
                    abonement.isTrial = int.Parse(value);
                    break;
                case "status_of_visible":
                    abonement.statusOfVisible = int.Parse(value);
                    break;
                case "status_for_app":
                    abonement.statusOfApp = int.Parse(value);
                    break;
                default:
                    break;
            }
            await _dbc.SaveChangesAsync();

            return true;
        }

        public async Task<bool> deleteFake(int id)
        {
            Abonement abonement = await findById(id);
            if (abonement == null) return false;
            abonement.statusOfDeleted = 1;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            Abonement abonement = await findById(id);
            if (abonement == null) return false;
            _dbc.Abonements.Remove(abonement);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Abonement abonement)
        {
            _dbc.Abonements.Remove(abonement);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
