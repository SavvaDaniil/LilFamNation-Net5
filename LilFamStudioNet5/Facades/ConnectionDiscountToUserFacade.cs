using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionDiscountToUser;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class ConnectionDiscountToUserFacade
    {
        public ApplicationDbContext _dbc;
        public ConnectionDiscountToUserFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<int>> listAllConnectedIdOfDiscountsByIdOfUser(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            return await listAllConnectedIdOfDiscountsByUser(user);
        }

        public async Task<List<int>> listAllConnectedIdOfDiscountsByUser(User user)
        {
            ConnectionDiscountToUserService connectionDiscountToUserService = new ConnectionDiscountToUserService(_dbc);
            List<ConnectionDiscountToUser> connectionDiscountToUsers = await connectionDiscountToUserService.listAllByUser(user);
            List<int> listAllConnectedIdOfDiscountsByUser = new List<int>();
            foreach (ConnectionDiscountToUser connectionDiscountToUser in connectionDiscountToUsers)
            {
                if (connectionDiscountToUser.discount == null) continue;
                listAllConnectedIdOfDiscountsByUser.Add(connectionDiscountToUser.discount.id);
            }

            return listAllConnectedIdOfDiscountsByUser;
        }

        public async Task<bool> update(ConnectionDiscountToUserUpdateDTO connectionDiscountToUserUpdateDTO)
        {
            DiscountService discountService = new DiscountService(_dbc);
            Discount discount = await discountService.findById(connectionDiscountToUserUpdateDTO.id_of_discount);
            if (discount == null) return false;

            UserService userService = new UserService(_dbc);
            User user = await userService.findById(connectionDiscountToUserUpdateDTO.id_of_user);
            if (user == null) return false;

            ConnectionDiscountToUserService connectionDiscountToUserService = new ConnectionDiscountToUserService(_dbc);
            if (connectionDiscountToUserUpdateDTO.status == 1)
            {
                ConnectionDiscountToUser connectionDiscountToUser = await connectionDiscountToUserService.add(discount, user);
                if (connectionDiscountToUser != null) return true;
            }
            else if (connectionDiscountToUserUpdateDTO.status == 0)
            {
                if (await connectionDiscountToUserService.delete(discount, user)) return true;
            }

            return false;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionDiscountToUserService connectionDiscountToUserService = new ConnectionDiscountToUserService(_dbc);
            ConnectionDiscountToUser connectionDiscountToUser = await connectionDiscountToUserService.findById(id);
            if (connectionDiscountToUser == null) return false;

            return await connectionDiscountToUserService.delete(connectionDiscountToUser);
        }
    }
}
