using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class DanceGroupService
    {
        public ApplicationDbContext _dbc;
        public DanceGroupService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<DanceGroup> findById(int id)
        {
            return await _dbc.DanceGroups
                .Include(p => p.teacher)
                .Include(p => p.branch)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<DanceGroup>> listAll()
        {
            return await _dbc.DanceGroups
                .Include(p => p.teacher)
                .Include(p => p.branch)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<DanceGroup>> listAllByName()
        {
            return await _dbc.DanceGroups
                .Include(p => p.teacher)
                .Include(p => p.branch)
                .OrderBy(p => p.name).ToListAsync();
        }

        public async Task<List<DanceGroup>> listAllActive()
        {
            return await _dbc.DanceGroups
                .Include(p => p.teacher)
                .Include(p => p.branch)
                .Where(p => p.status == 1)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<DanceGroup>> listAllActiveForApp()
        {
            return await _dbc.DanceGroups
                .Include(p => p.teacher)
                .Include(p => p.branch)
                .Where(p => p.status == 1 && p.statusOfApp == 1)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<DanceGroup> add(string name)
        {
            DanceGroup danceGroup = new DanceGroup();
            danceGroup.name = name;

            await _dbc.DanceGroups.AddAsync(danceGroup);
            await _dbc.SaveChangesAsync();

            return danceGroup;
        }


        public async Task<bool> updateByColumn(DanceGroup danceGroup, string name, string value, Teacher teacher = null, Branch branch = null)
        {
            switch (name)
            {
                case "name":
                    danceGroup.name = value;
                    break;
                case "description":
                    danceGroup.description = value;
                    break;
                case "status":
                    danceGroup.status = int.Parse(value);
                    break;
                case "isCreative":
                    danceGroup.statusOfCreative = int.Parse(value);
                    break;
                case "statusOfApp":
                    danceGroup.statusOfApp = int.Parse(value);
                    break;
                case "isAbonementsAllowAll":
                    danceGroup.isAbonementsAllowAll = int.Parse(value);
                    break;
                case "isActiveReservation":
                    danceGroup.isActiveReservation = int.Parse(value);
                    break;
                case "isEvent":
                    danceGroup.isEvent = int.Parse(value);
                    break;
                case "teacher":
                    danceGroup.teacher = teacher;
                    break;
                case "branch":
                    danceGroup.branch = branch;
                    break;
                default:
                    break;
            }
            await _dbc.SaveChangesAsync();

            return true;
        }

        public async Task<bool> delete(int id)
        {
            DanceGroup danceGroup = await findById(id);
            if (danceGroup == null) return false;
            _dbc.DanceGroups.Remove(danceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(DanceGroup danceGroup)
        {
            _dbc.DanceGroups.Remove(danceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
