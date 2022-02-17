using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionAbonementPrivateToUser;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class ConnectionAbonementPrivateToUserFacade
    {
        public ApplicationDbContext _dbc;
        public ConnectionAbonementPrivateToUserFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<int>> listAllConnectedIdOfAbonementPrivatesByIdOfUser(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            return await listAllConnectedIdOfAbonementPrivatesByUser(user);
        }

        public async Task<List<int>> listAllConnectedIdOfAbonementPrivatesByUser(User user)
        {
            ConnectionAbonementPrivateToUserService connectionAbonementPrivateToUserService = new ConnectionAbonementPrivateToUserService(_dbc);
            List<ConnectionAbonementPrivateToUser> connectionAbonementPrivateToUsers = await connectionAbonementPrivateToUserService.listAllByUser(user);
            List<int> listAllConnectedIdOfAbonementPrivatesByUser = new List<int>();
            foreach (ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser in connectionAbonementPrivateToUsers)
            {
                if (connectionAbonementPrivateToUser.abonement == null) continue;
                listAllConnectedIdOfAbonementPrivatesByUser.Add(connectionAbonementPrivateToUser.abonement.id);
            }

            return listAllConnectedIdOfAbonementPrivatesByUser;
        }

        public async Task<HashSet<int>> hashAllConnectedIdOfAbonementPrivatesByUser(User user)
        {
            ConnectionAbonementPrivateToUserService connectionAbonementPrivateToUserService = new ConnectionAbonementPrivateToUserService(_dbc);
            List<ConnectionAbonementPrivateToUser> connectionAbonementPrivateToUsers = await connectionAbonementPrivateToUserService.listAllByUser(user);
            HashSet<int> hashAllConnectedIdOfAbonementPrivatesByUser = new HashSet<int>();
            foreach (ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser in connectionAbonementPrivateToUsers)
            {
                if (connectionAbonementPrivateToUser.abonement == null) continue;
                hashAllConnectedIdOfAbonementPrivatesByUser.Add(connectionAbonementPrivateToUser.abonement.id);
            }

            return hashAllConnectedIdOfAbonementPrivatesByUser;
        }

        public async Task<bool> update(ConnectionAbonementPrivateToUserUpdateDTO connectionAbonementPrivateToUserUpdateDTO)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(connectionAbonementPrivateToUserUpdateDTO.id_of_abonement);
            if (abonement == null) return false;

            UserService userService = new UserService(_dbc);
            User user = await userService.findById(connectionAbonementPrivateToUserUpdateDTO.id_of_user);
            if (user == null) return false;

            ConnectionAbonementPrivateToUserService connectionAbonementPrivateToUserService = new ConnectionAbonementPrivateToUserService(_dbc);
            if (connectionAbonementPrivateToUserUpdateDTO.status == 1)
            {
                ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser = await connectionAbonementPrivateToUserService.add(abonement, user);
                if (connectionAbonementPrivateToUser != null) return true;
            }
            else if (connectionAbonementPrivateToUserUpdateDTO.status == 0)
            {
                if (await connectionAbonementPrivateToUserService.delete(abonement, user)) return true;
            }

            return false;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionAbonementPrivateToUserService connectionAbonementPrivateToUserService = new ConnectionAbonementPrivateToUserService(_dbc);
            ConnectionAbonementPrivateToUser connectionAbonementPrivateToUser = await connectionAbonementPrivateToUserService.findById(id);
            if (connectionAbonementPrivateToUser == null) return false;

            return await connectionAbonementPrivateToUserService.delete(connectionAbonementPrivateToUser);
        }
    }
}
