using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class BranchService
    {
        public ApplicationDbContext _dbc;
        public BranchService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<Branch> findById(int id)
        {
            return await _dbc.Branches.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<Branch>> listAll()
        {
            return await _dbc.Branches.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<Branch> add(string name)
        {
            Branch branch = new Branch();
            branch.name = name;

            await _dbc.Branches.AddAsync(branch);
            await _dbc.SaveChangesAsync();

            return branch;
        }

        public async Task<bool> updateByColumn(Branch branch, string name, string value)
        {
            switch (name)
            {
                case "name":
                    branch.name = value;
                    break;
                case "description":
                    branch.description = value;
                    break;
                case "coordinates":
                    branch.coordinates = value;
                    break;
                default:
                    break;
            }
            await _dbc.SaveChangesAsync();

            return true;
        }

        public async Task<bool> delete(int id)
        {
            Branch branch = await findById(id);
            if (branch == null) return false;
            _dbc.Branches.Remove(branch);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Branch branch)
        {
            _dbc.Branches.Remove(branch);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
