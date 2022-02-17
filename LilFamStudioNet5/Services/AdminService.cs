using LilFamStudioNet5.Components;
using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Admin;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class AdminService
    {
        public ApplicationDbContext _dbc;
        public AdminService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<Admin> findById(int id)
        {
            return await _dbc.Admins.FirstOrDefaultAsync(p => p.id == id);
        }
        public async Task<Admin> findByUsername(string username)
        {
            return await _dbc.Admins.FirstOrDefaultAsync(p => p.Username == username);
        }

        public bool isAlreadyExistByUsername(string username)
        {
            if (_dbc.Admins.Any(p => p.Username == username))
            {
                return true;
            }
            return false;
        }

        public bool isAlreadyExistByUsernameExceptId(int id, string username)
        {
            if (_dbc.Admins.Where(p => p.Username == username).Where(p => p.id != id).Any())
            {
                return true;
            }
            return false;
        }

        public async Task<Admin> add(string username, string password = null)
        {
            Admin admin = new Admin();
            admin.Username = username;
            admin.dateOfAdd = DateTime.Now;
            admin.Password = BCrypt.Net.BCrypt.HashPassword((password != null ? password : RandomComponent.RandomString(6)));
            admin.AuthKey = RandomComponent.RandomString(32);

            await _dbc.Admins.AddAsync(admin);
            await _dbc.SaveChangesAsync();

            return admin;
        }

        public async Task<bool> updateEdit(
            Admin admin,
            string username,
            string name,
            string position,
            string newPassword,
            int active,
            int panelAdmins,
            int panelLessons,
            int panelUsers,
            int panelDanceGroups,
            int panelTeachers,
            int panelAbonements,
            int panelDiscounts,
            int panelBranches
        )
        {
            admin.Username = username;
            admin.name = name;
            admin.position = position;
            if (newPassword != null)
            {
                admin.Password = newPassword;
                admin.AuthKey = RandomComponent.RandomString(32);
            }
            admin.active = active;


            admin.panelAdmins = panelAdmins;
            admin.panelLessons = panelLessons;
            admin.panelUsers = panelUsers;
            admin.panelDanceGroups = panelDanceGroups;
            admin.panelTeachers = panelTeachers;
            admin.panelAbonements = panelAbonements;
            admin.panelDiscounts = panelDiscounts;
            admin.panelBranches = panelBranches;

            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> update(
            Admin admin,
            string username,
            string name,
            string position,
            string password
        )
        {
            admin.Username = username;
            admin.name = name;
            admin.position = position;
            if (password != null)
            {
                admin.Password = BCrypt.Net.BCrypt.HashPassword(password);
                admin.AuthKey = RandomComponent.RandomString(32);
            }
            await _dbc.SaveChangesAsync();
            return true;
        }


        public async Task<List<Admin>> searchAdmins(AdminSearchDTO adminSearchDTO)
        {
            adminSearchDTO.page--;
            IQueryable<Admin> q = _dbc.Admins.OrderByDescending(p => p.id);

            q = q.Take(30).Skip(adminSearchDTO.page * 30);
            q = q.Where(p => p.level == 0);

            if (!string.IsNullOrEmpty(adminSearchDTO.queryString))
            {
                q = q.Where(p =>
                    EF.Functions.Like(p.Username, adminSearchDTO.queryString)
                    || EF.Functions.Like(p.position, adminSearchDTO.queryString)
                    || p.Username.Contains(adminSearchDTO.queryString)
                    || p.position.Contains(adminSearchDTO.queryString)
                );
            }
            return await q.ToListAsync();
        }

        public int searchCount(AdminSearchDTO adminSearchDTO)
        {
            IQueryable<Admin> q = _dbc.Admins.OrderByDescending(p => p.id);

            q = q.Where(p => p.level == 0);

            if (!string.IsNullOrEmpty(adminSearchDTO.queryString))
            {
                q = q.Where(p =>
                    EF.Functions.Like(p.Username, adminSearchDTO.queryString)
                    || EF.Functions.Like(p.position, adminSearchDTO.queryString)
                    || p.Username.Contains(adminSearchDTO.queryString)
                    || p.position.Contains(adminSearchDTO.queryString)
                );
            }
            return q.Count();
        }


        public async Task<bool> delete(int id)
        {
            Admin admin = await findById(id);
            if (admin == null) return false;
            _dbc.Admins.Remove(admin);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
