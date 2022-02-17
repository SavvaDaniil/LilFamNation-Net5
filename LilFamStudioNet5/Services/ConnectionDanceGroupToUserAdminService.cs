using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class ConnectionDanceGroupToUserAdminService
    {
        public ApplicationDbContext _dbc;
        public ConnectionDanceGroupToUserAdminService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<ConnectionDanceGroupToUserAdmin> findById(int id)
        {
            return await _dbc.ConnectionsDanceGroupToUserAdmin
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ConnectionDanceGroupToUserAdmin> findByDanceGroupAndUser(DanceGroup danceGroup, User user)
        {
            return await _dbc.ConnectionsDanceGroupToUserAdmin
                .FirstOrDefaultAsync(p => p.danceGroup == danceGroup && p.user == user);
        }

        public async Task<bool> isAnyByUserAndDanceGroup(User user, DanceGroup danceGroup)
        {
            return await _dbc.ConnectionsDanceGroupToUserAdmin
                .Where(p => p.user == user && p.danceGroup == danceGroup)
                .AnyAsync();
        }

        public async Task<List<ConnectionDanceGroupToUserAdmin>> listAll()
        {
            return await _dbc.ConnectionsDanceGroupToUserAdmin
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<ConnectionDanceGroupToUserAdmin>> listAllByUser(User user)
        {
            return await _dbc.ConnectionsDanceGroupToUserAdmin
                .Where(p => p.user == user)
                .Include(p => p.danceGroup)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<ConnectionDanceGroupToUserAdmin> add(DanceGroup danceGroup, User user)
        {
            ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin = await findByDanceGroupAndUser(danceGroup, user);
            if (connectionDanceGroupToUserAdmin != null) return connectionDanceGroupToUserAdmin;

            connectionDanceGroupToUserAdmin = new ConnectionDanceGroupToUserAdmin();
            connectionDanceGroupToUserAdmin.user = user;
            connectionDanceGroupToUserAdmin.danceGroup = danceGroup;
            connectionDanceGroupToUserAdmin.dateOfAdd = DateTime.Now;

            await _dbc.ConnectionsDanceGroupToUserAdmin.AddAsync(connectionDanceGroupToUserAdmin);
            await _dbc.SaveChangesAsync();

            return connectionDanceGroupToUserAdmin;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin = await findById(id);
            if (connectionDanceGroupToUserAdmin == null) return false;
            _dbc.ConnectionsDanceGroupToUserAdmin.Remove(connectionDanceGroupToUserAdmin);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin)
        {
            _dbc.ConnectionsDanceGroupToUserAdmin.Remove(connectionDanceGroupToUserAdmin);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(DanceGroup danceGroup, User user)
        {
            ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin = await findByDanceGroupAndUser(danceGroup, user);
            _dbc.ConnectionsDanceGroupToUserAdmin.Remove(connectionDanceGroupToUserAdmin);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
