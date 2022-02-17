using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class ConnectionAbonementPrivateToUserService
    {
        public ApplicationDbContext _dbc;
        public ConnectionAbonementPrivateToUserService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<ConnectionAbonementPrivateToUser> findById(int id)
        {
            return await _dbc.ConnectionsAbonementPrivateToUser
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ConnectionAbonementPrivateToUser> findByAbonementPrivateAndUser(Abonement abonement, User user)
        {
            return await _dbc.ConnectionsAbonementPrivateToUser
                .FirstOrDefaultAsync(p => p.abonement == abonement && p.user == user);
        }

        public async Task<List<ConnectionAbonementPrivateToUser>> listAll()
        {
            return await _dbc.ConnectionsAbonementPrivateToUser
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<ConnectionAbonementPrivateToUser>> listAllByUser(User user)
        {
            return await _dbc.ConnectionsAbonementPrivateToUser
                .Where(p => p.user == user)
                .Include(p => p.abonement)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<ConnectionAbonementPrivateToUser> add(Abonement abonement, User user)
        {
            ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser = await findByAbonementPrivateAndUser(abonement, user);
            if (connectionAbonementPrivateToUser != null) return connectionAbonementPrivateToUser;

            connectionAbonementPrivateToUser = new ConnectionAbonementPrivateToUser();
            connectionAbonementPrivateToUser.user = user;
            connectionAbonementPrivateToUser.abonement = abonement;
            connectionAbonementPrivateToUser.dateOfAdd = DateTime.Now;

            await _dbc.ConnectionsAbonementPrivateToUser.AddAsync(connectionAbonementPrivateToUser);
            await _dbc.SaveChangesAsync();

            return connectionAbonementPrivateToUser;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser = await findById(id);
            if (connectionAbonementPrivateToUser == null) return false;
            _dbc.ConnectionsAbonementPrivateToUser.Remove(connectionAbonementPrivateToUser);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser)
        {
            _dbc.ConnectionsAbonementPrivateToUser.Remove(connectionAbonementPrivateToUser);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Abonement AbonementPrivate, User user)
        {
            ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser = await findByAbonementPrivateAndUser(AbonementPrivate, user);
            _dbc.ConnectionsAbonementPrivateToUser.Remove(connectionAbonementPrivateToUser);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
