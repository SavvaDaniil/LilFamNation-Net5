using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class ConnectionAbonementToDanceGroupService
    {
        public ApplicationDbContext _dbc;
        public ConnectionAbonementToDanceGroupService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<ConnectionAbonementToDanceGroup> findById(int id)
        {
            return await _dbc.ConnectionsAbonementToDanceGroup
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ConnectionAbonementToDanceGroup> findByAbonementAndDanceGroup(Abonement abonement, DanceGroup danceGroup)
        {
            return await _dbc.ConnectionsAbonementToDanceGroup
                .FirstOrDefaultAsync(p => p.abonement == abonement && p.danceGroup == danceGroup);
        }

        public async Task<List<ConnectionAbonementToDanceGroup>> listAll()
        {
            return await _dbc.ConnectionsAbonementToDanceGroup
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<ConnectionAbonementToDanceGroup>> listAllByDanceGroup(DanceGroup danceGroup)
        {
            return await _dbc.ConnectionsAbonementToDanceGroup
                .Where(p => p.danceGroup == danceGroup)
                .Include(p => p.abonement)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<ConnectionAbonementToDanceGroup> add(Abonement abonement, DanceGroup danceGroup)
        {
            ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup = await findByAbonementAndDanceGroup(abonement, danceGroup);
            if (connectionAbonementToDanceGroup != null) return connectionAbonementToDanceGroup;

            connectionAbonementToDanceGroup = new ConnectionAbonementToDanceGroup();
            connectionAbonementToDanceGroup.danceGroup = danceGroup;
            connectionAbonementToDanceGroup.abonement = abonement;
            connectionAbonementToDanceGroup.dateOfAdd = DateTime.Now;

            await _dbc.ConnectionsAbonementToDanceGroup.AddAsync(connectionAbonementToDanceGroup);
            await _dbc.SaveChangesAsync();

            return connectionAbonementToDanceGroup;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup = await findById(id);
            if (connectionAbonementToDanceGroup == null) return false;
            _dbc.ConnectionsAbonementToDanceGroup.Remove(connectionAbonementToDanceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup)
        {
            _dbc.ConnectionsAbonementToDanceGroup.Remove(connectionAbonementToDanceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Abonement abonement, DanceGroup danceGroup)
        {
            ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup = await findByAbonementAndDanceGroup(abonement, danceGroup);
            _dbc.ConnectionsAbonementToDanceGroup.Remove(connectionAbonementToDanceGroup);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
