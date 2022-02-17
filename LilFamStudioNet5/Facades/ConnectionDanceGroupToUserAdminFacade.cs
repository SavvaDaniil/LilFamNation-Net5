using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionDanceGroupToUserAdmin;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class ConnectionDanceGroupToUserAdminFacade
    {
        public ApplicationDbContext _dbc;
        public ConnectionDanceGroupToUserAdminFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<int>> listAllConnectedIdOfDanceGroupsByIdOfUser(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            return await listAllConnectedIdOfDanceGroupsByUser(user);
        }

        public async Task<List<int>> listAllConnectedIdOfDanceGroupsByUser(User user)
        {
            ConnectionDanceGroupToUserAdminService connectionDanceGroupToUserAdminService = new ConnectionDanceGroupToUserAdminService(_dbc);
            List<ConnectionDanceGroupToUserAdmin> connectionDanceGroupToUserAdmins = await connectionDanceGroupToUserAdminService.listAllByUser(user);
            List<int> listAllConnectedIdOfDanceGroupsByUser = new List<int>();
            foreach (ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin in connectionDanceGroupToUserAdmins)
            {
                if (connectionDanceGroupToUserAdmin.danceGroup == null) continue;
                listAllConnectedIdOfDanceGroupsByUser.Add(connectionDanceGroupToUserAdmin.danceGroup.id);
            }

            return listAllConnectedIdOfDanceGroupsByUser;
        }

        public async Task<HashSet<int>> hashOfAllConnectedIdOfDanceGroupsByUser(User user)
        {
            ConnectionDanceGroupToUserAdminService connectionDanceGroupToUserAdminService = new ConnectionDanceGroupToUserAdminService(_dbc);
            List<ConnectionDanceGroupToUserAdmin> connectionDanceGroupToUserAdmins = await connectionDanceGroupToUserAdminService.listAllByUser(user);
            HashSet<int> hashOfAllConnectedIdOfDanceGroupsByUser = new HashSet<int>();
            foreach (ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin in connectionDanceGroupToUserAdmins)
            {
                hashOfAllConnectedIdOfDanceGroupsByUser.Add(connectionDanceGroupToUserAdmin.danceGroup.id);
            }

            return hashOfAllConnectedIdOfDanceGroupsByUser;
        }

        public async Task<bool> isUserAdminConnectedToDanceGroup(User user, DanceGroup danceGroup)
        {
            ConnectionDanceGroupToUserAdminService connectionDanceGroupToUserAdminService = new ConnectionDanceGroupToUserAdminService(_dbc);
            return await connectionDanceGroupToUserAdminService.isAnyByUserAndDanceGroup(user, danceGroup);
        }


        public async Task<bool> update(ConnectionDanceGroupToUserAdminUpdateDTO connectionDanceGroupToUserAdminUpdateDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(connectionDanceGroupToUserAdminUpdateDTO.id_of_dance_group);
            if (danceGroup == null) return false;

            UserService userService = new UserService(_dbc);
            User user = await userService.findById(connectionDanceGroupToUserAdminUpdateDTO.id_of_user);
            if (user == null) return false;

            ConnectionDanceGroupToUserAdminService connectionDanceGroupToUserAdminService = new ConnectionDanceGroupToUserAdminService(_dbc);
            if (connectionDanceGroupToUserAdminUpdateDTO.status == 1)
            {
                ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin = await connectionDanceGroupToUserAdminService.add(danceGroup, user);
                if (connectionDanceGroupToUserAdmin != null) return true;
            }
            else if (connectionDanceGroupToUserAdminUpdateDTO.status == 0)
            {
                if (await connectionDanceGroupToUserAdminService.delete(danceGroup, user)) return true;
            }

            return false;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionDanceGroupToUserAdminService connectionDanceGroupToUserAdminService = new ConnectionDanceGroupToUserAdminService(_dbc);
            ConnectionDanceGroupToUserAdmin connectionDanceGroupToUserAdmin = await connectionDanceGroupToUserAdminService.findById(id);
            if (connectionDanceGroupToUserAdmin == null) return false;

            return await connectionDanceGroupToUserAdminService.delete(connectionDanceGroupToUserAdmin);
        }
    }
}
