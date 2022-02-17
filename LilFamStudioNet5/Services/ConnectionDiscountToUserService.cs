using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class ConnectionDiscountToUserService
    {
        public ApplicationDbContext _dbc;
        public ConnectionDiscountToUserService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<ConnectionDiscountToUser> findById(int id)
        {
            return await _dbc.ConnectionsDiscountToUser
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ConnectionDiscountToUser> findByDiscountAndUser(Discount discount, User user)
        {
            return await _dbc.ConnectionsDiscountToUser
                .FirstOrDefaultAsync(p => p.discount == discount && p.user == user);
        }

        public async Task<List<ConnectionDiscountToUser>> listAll()
        {
            return await _dbc.ConnectionsDiscountToUser
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<ConnectionDiscountToUser>> listAllByUser(User user)
        {
            return await _dbc.ConnectionsDiscountToUser
                .Where(p => p.user == user)
                .Include(p => p.discount)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<ConnectionDiscountToUser> add(Discount discount, User user)
        {
            ConnectionDiscountToUser connectionDiscountToUser = await findByDiscountAndUser(discount, user);
            if (connectionDiscountToUser != null) return connectionDiscountToUser;

            connectionDiscountToUser = new ConnectionDiscountToUser();
            connectionDiscountToUser.user = user;
            connectionDiscountToUser.discount = discount;
            connectionDiscountToUser.dateOfAdd = DateTime.Now;

            await _dbc.ConnectionsDiscountToUser.AddAsync(connectionDiscountToUser);
            await _dbc.SaveChangesAsync();

            return connectionDiscountToUser;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionDiscountToUser connectionDiscountToUser = await findById(id);
            if (connectionDiscountToUser == null) return false;
            _dbc.ConnectionsDiscountToUser.Remove(connectionDiscountToUser);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(ConnectionDiscountToUser connectionDiscountToUser)
        {
            _dbc.ConnectionsDiscountToUser.Remove(connectionDiscountToUser);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Discount discount, User user)
        {
            ConnectionDiscountToUser connectionDiscountToUser = await findByDiscountAndUser(discount, user);
            _dbc.ConnectionsDiscountToUser.Remove(connectionDiscountToUser);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
