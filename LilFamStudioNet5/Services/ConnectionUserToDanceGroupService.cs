using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class ConnectionUserToDanceGroupService
    {
        public ApplicationDbContext _dbc;
        public ConnectionUserToDanceGroupService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<ConnectionUserToDanceGroup> findById(int id)
        {
            return await _dbc.ConnectionsUserToDanceGroup
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ConnectionUserToDanceGroup> findByDanceGroupAndUser(DanceGroup danceGroup, User user)
        {
            return await _dbc.ConnectionsUserToDanceGroup
                .FirstOrDefaultAsync(p => p.danceGroup == danceGroup && p.user == user);
        }

        public async Task<bool> isAnyDanceGroupAndUser(User user, DanceGroup danceGroup)
        {
            return await _dbc.ConnectionsUserToDanceGroup
                .Where(p => p.danceGroup == danceGroup && p.user == user)
                .AnyAsync();
        }

        public async Task<List<ConnectionUserToDanceGroup>> listAll()
        {
            return await _dbc.ConnectionsUserToDanceGroup
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<ConnectionUserToDanceGroup>> listAllByDanceGroup(DanceGroup danceGroup, bool onlyLast30Days = false)
        {
            if (onlyLast30Days)
            {
                return await _dbc.ConnectionsUserToDanceGroup
                    .Where(p => p.danceGroup == danceGroup && p.dateOfUpdate >= (DateTime.Now.Date.AddDays(-30)))
                    .Include(p => p.user)
                    .OrderByDescending(p => p.id)
                    .ToListAsync();

            } else
            {
                return await _dbc.ConnectionsUserToDanceGroup
                    .Where(p => p.danceGroup == danceGroup)
                    .Include(p => p.user)
                    .OrderByDescending(p => p.id)
                    .ToListAsync();
            }
        }

        public async Task<List<ConnectionUserToDanceGroup>> listAllByUser(User user)
        {
            return await _dbc.ConnectionsUserToDanceGroup
                .Where(p => p.user == user)
                .Include(p => p.danceGroup)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<ConnectionUserToDanceGroup> add(DanceGroup danceGroup, User user)
        {
            ConnectionUserToDanceGroup connectionUserToDanceGroup = await findByDanceGroupAndUser(danceGroup, user);
            if (connectionUserToDanceGroup != null) return connectionUserToDanceGroup;

            connectionUserToDanceGroup = new ConnectionUserToDanceGroup();
            connectionUserToDanceGroup.user = user;
            connectionUserToDanceGroup.danceGroup = danceGroup;
            connectionUserToDanceGroup.dateOfAdd = DateTime.Now;
            connectionUserToDanceGroup.dateOfUpdate = DateTime.Now;

            await _dbc.ConnectionsUserToDanceGroup.AddAsync(connectionUserToDanceGroup);
            await _dbc.SaveChangesAsync();

            return connectionUserToDanceGroup;
        }

        public async Task<bool> update(ConnectionUserToDanceGroup connectionUserToDanceGroup)
        {
            connectionUserToDanceGroup.dateOfUpdate = DateTime.Now;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionUserToDanceGroup connectionUserToDanceGroup = await findById(id);
            if (connectionUserToDanceGroup == null) return false;
            _dbc.ConnectionsUserToDanceGroup.Remove(connectionUserToDanceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(ConnectionUserToDanceGroup connectionUserToDanceGroup)
        {
            _dbc.ConnectionsUserToDanceGroup.Remove(connectionUserToDanceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(DanceGroup danceGroup, User user)
        {
            ConnectionUserToDanceGroup connectionUserToDanceGroup = await findByDanceGroupAndUser(danceGroup, user);
            _dbc.ConnectionsUserToDanceGroup.Remove(connectionUserToDanceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
