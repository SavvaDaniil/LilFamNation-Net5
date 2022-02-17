using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class DiscountService
    {
        public ApplicationDbContext _dbc;
        public DiscountService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<Discount> findById(int id)
        {
            return await _dbc.Discounts.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<Discount>> listAll()
        {
            return await _dbc.Discounts.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<Discount> add(string name)
        {
            Discount discount = new Discount();
            discount.name = name;
            discount.dateOfAdd = DateTime.Now;

            await _dbc.Discounts.AddAsync(discount);
            await _dbc.SaveChangesAsync();

            return discount;
        }

        public async Task<bool> updateByColumn(Discount discount, string name, string value)
        {
            switch (name)
            {
                case "name":
                    discount.name = value;
                    break;
                default:
                    break;
            }
            await _dbc.SaveChangesAsync();

            return true;
        }

        public async Task<bool> delete(int id)
        {
            Discount discount = await findById(id);
            if (discount == null) return false;
            _dbc.Discounts.Remove(discount);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Discount discount)
        {
            _dbc.Discounts.Remove(discount);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
