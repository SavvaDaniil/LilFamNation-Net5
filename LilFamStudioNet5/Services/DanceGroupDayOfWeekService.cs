using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class DanceGroupDayOfWeekService
    {
        public ApplicationDbContext _dbc;
        public DanceGroupDayOfWeekService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<DanceGroupDayOfWeek> findById(int id)
        {
            return await _dbc.DanceGroupDayOfWeeks
                .Include(p => p.danceGroup)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<DanceGroupDayOfWeek>> listAll()
        {
            return await _dbc.DanceGroupDayOfWeeks
                .OrderByDescending(p => p.id)
                .ToListAsync();
        }

        public async Task<List<DanceGroupDayOfWeek>> listAllOrderByTimeFrom()
        {
            return await _dbc.DanceGroupDayOfWeeks
                .Include(p => p.danceGroup)
                .OrderBy(p => p.timeFrom)
                .ToListAsync();
        }

        public async Task<List<DanceGroupDayOfWeek>> listAllGroubByTimeFrom()
        {
            return await _dbc.DanceGroupDayOfWeeks
                .OrderByDescending(p => p.timeFrom)
                .ToListAsync();
        }

        public async Task<List<DanceGroupDayOfWeek>> listAllByDanceGroup(DanceGroup danceGroup)
        {
            return await _dbc.DanceGroupDayOfWeeks
                .Where(p => p.danceGroup == danceGroup)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<DanceGroupDayOfWeek>> listAllByDanceGroupOrderByTimeFrom(DanceGroup danceGroup)
        {
            return await _dbc.DanceGroupDayOfWeeks
                .Where(p => p.danceGroup == danceGroup)
                .OrderBy(p => p.timeFrom)
                .ToListAsync();
        }

        public async Task<DanceGroupDayOfWeek> add(DanceGroup danceGroup)
        {
            DanceGroupDayOfWeek danceGroupDayOfWeek = new DanceGroupDayOfWeek();
            danceGroupDayOfWeek.danceGroup = danceGroup;
            danceGroupDayOfWeek.dayOfWeek = 1;
            danceGroupDayOfWeek.dateOfAdd = DateTime.Now;
            danceGroupDayOfWeek.dateOfUpdate = DateTime.Now;

            await _dbc.DanceGroupDayOfWeeks.AddAsync(danceGroupDayOfWeek);
            await _dbc.SaveChangesAsync();

            return danceGroupDayOfWeek;
        }

        public async Task<bool> updateByColumn(DanceGroupDayOfWeek danceGroupDayOfWeek, string name, int value, TimeSpan? time)
        {
            switch (name)
            {
                case "status":
                    danceGroupDayOfWeek.status = value;
                    break;
                case "dayOfWeek":
                    danceGroupDayOfWeek.dayOfWeek = value;
                    break;
                case "timeFrom":
                    danceGroupDayOfWeek.timeFrom = time;
                    break;
                case "timeTo":
                    danceGroupDayOfWeek.timeTo = time;
                    break;
                default:
                    break;
            }
            danceGroupDayOfWeek.dateOfUpdate = DateTime.Now;
            await _dbc.SaveChangesAsync();

            return true;
        }

        public async Task<bool> delete(int id)
        {
            DanceGroupDayOfWeek danceGroupDayOfWeek = await findById(id);
            if (danceGroupDayOfWeek == null) return false;
            _dbc.DanceGroupDayOfWeeks.Remove(danceGroupDayOfWeek);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            _dbc.DanceGroupDayOfWeeks.Remove(danceGroupDayOfWeek);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
