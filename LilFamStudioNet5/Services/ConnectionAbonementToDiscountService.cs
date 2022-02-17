using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class ConnectionAbonementToDiscountService
    {
        public ApplicationDbContext _dbc;
        public ConnectionAbonementToDiscountService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<ConnectionAbonementToDiscount> findById(int id)
        {
            return await _dbc.ConnectionsAbonementToDiscount
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<ConnectionAbonementToDiscount> findByAbonementAndDiscount(Abonement abonement, Discount discount)
        {
            return await _dbc.ConnectionsAbonementToDiscount
                .FirstOrDefaultAsync(p => p.abonement == abonement && p.discount == discount);
        }

        public async Task<List<ConnectionAbonementToDiscount>> listAll()
        {
            return await _dbc.ConnectionsAbonementToDiscount
                .Include(p => p.abonement)
                .Include(p => p.discount)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<ConnectionAbonementToDiscount>> listAllByDiscount(Discount discount)
        {
            return await _dbc.ConnectionsAbonementToDiscount
                .Where(p => p.discount == discount)
                .Include(p => p.abonement)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<ConnectionAbonementToDiscount> add(Abonement abonement, Discount discount)
        {
            ConnectionAbonementToDiscount connectionAbonementToDiscount = await findByAbonementAndDiscount(abonement, discount);
            if (connectionAbonementToDiscount != null) return connectionAbonementToDiscount;

            connectionAbonementToDiscount = new ConnectionAbonementToDiscount();
            connectionAbonementToDiscount.discount = discount;
            connectionAbonementToDiscount.abonement = abonement;
            connectionAbonementToDiscount.dateOfAdd = DateTime.Now;

            await _dbc.ConnectionsAbonementToDiscount.AddAsync(connectionAbonementToDiscount);
            await _dbc.SaveChangesAsync();

            return connectionAbonementToDiscount;
        }

        public async Task<bool> update(ConnectionAbonementToDiscount connectionAbonementToDiscount, int procent)
        {
            if (procent < 0 || procent > 99) procent = 0;

            connectionAbonementToDiscount.value = procent;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionAbonementToDiscount connectionAbonementToDiscount = await findById(id);
            if (connectionAbonementToDiscount == null) return false;
            _dbc.ConnectionsAbonementToDiscount.Remove(connectionAbonementToDiscount);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(ConnectionAbonementToDiscount connectionAbonementToDiscount)
        {
            _dbc.ConnectionsAbonementToDiscount.Remove(connectionAbonementToDiscount);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Abonement abonement, Discount discount)
        {
            ConnectionAbonementToDiscount connectionAbonementToDiscount = await findByAbonementAndDiscount(abonement, discount);
            _dbc.ConnectionsAbonementToDiscount.Remove(connectionAbonementToDiscount);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
