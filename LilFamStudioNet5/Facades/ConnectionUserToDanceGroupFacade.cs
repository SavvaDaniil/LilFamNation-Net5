using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionUserToDanceGroup;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class ConnectionUserToDanceGroupFacade
    {
        public ApplicationDbContext _dbc;
        public ConnectionUserToDanceGroupFacade(ApplicationDbContext dbc)
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
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            List<ConnectionUserToDanceGroup> connectionUserToDanceGroups = await connectionUserToDanceGroupService.listAllByUser(user);
            List<int> listAllConnectedIdOfDanceGroupsByUser = new List<int>();
            foreach (ConnectionUserToDanceGroup ConnectionUserToDanceGroup in connectionUserToDanceGroups)
            {
                if (ConnectionUserToDanceGroup.danceGroup == null) continue;
                listAllConnectedIdOfDanceGroupsByUser.Add(ConnectionUserToDanceGroup.danceGroup.id);
            }

            return listAllConnectedIdOfDanceGroupsByUser;
        }

        public async Task<List<DanceGroup>> listAllDanceGroupsConnectedToUser(User user)
        {
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            List<ConnectionUserToDanceGroup> connectionUserToDanceGroups = await connectionUserToDanceGroupService.listAllByUser(user);
            List<DanceGroup> danceGroups = new List<DanceGroup>();
            foreach (ConnectionUserToDanceGroup connectionUserToDanceGroup in connectionUserToDanceGroups)
            {
                if (danceGroups.Contains(connectionUserToDanceGroup.danceGroup)) continue;
                danceGroups.Add(connectionUserToDanceGroup.danceGroup);
            }

            return danceGroups;
        }


        public async Task<List<User>> listAllUsersConnectedToDanceGroup(DanceGroup danceGroup, bool onlyLast30Days = false)
        {
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            List<ConnectionUserToDanceGroup> connectionUserToDanceGroups = await connectionUserToDanceGroupService.listAllByDanceGroup(danceGroup, onlyLast30Days);
            List<User> users = new List<User>();
            foreach (ConnectionUserToDanceGroup ConnectionUserToDanceGroup in connectionUserToDanceGroups)
            {
                if (ConnectionUserToDanceGroup.user == null) continue;
                users.Add(ConnectionUserToDanceGroup.user);
            }
            return users;
        }

        public async Task<List<int>> listAllIdOfUserConnectedToDanceGroup(DanceGroup danceGroup, bool onlyLast30Days = false)
        {
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            List<ConnectionUserToDanceGroup> connectionUserToDanceGroups = await connectionUserToDanceGroupService.listAllByDanceGroup(danceGroup, onlyLast30Days);
            List<int> listAllIdOfUserConnectedToDanceGroup = new List<int>();
            foreach (ConnectionUserToDanceGroup ConnectionUserToDanceGroup in connectionUserToDanceGroups)
            {
                if (ConnectionUserToDanceGroup.user == null) continue;
                listAllIdOfUserConnectedToDanceGroup.Add(ConnectionUserToDanceGroup.user.id);
            }
            return listAllIdOfUserConnectedToDanceGroup;
        }

        public async Task<bool> update(ConnectionUserToDanceGroupUpdateDTO connectionUserToDanceGroupUpdateDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(connectionUserToDanceGroupUpdateDTO.id_of_dance_group);
            if (danceGroup == null) return false;

            UserService userService = new UserService(_dbc);
            User user = await userService.findById(connectionUserToDanceGroupUpdateDTO.id_of_user);
            if (user == null) return false;

            return await update(user, danceGroup, connectionUserToDanceGroupUpdateDTO.status);
        }

        public async Task<bool> update(User user, DanceGroup danceGroup, int status = 1)
        {
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            if (status == 1)
            {
                ConnectionUserToDanceGroup connectionUserToDanceGroup = await connectionUserToDanceGroupService.findByDanceGroupAndUser(danceGroup, user);
                if (connectionUserToDanceGroup == null)
                {
                    connectionUserToDanceGroup = await connectionUserToDanceGroupService.add(danceGroup, user);
                    if (connectionUserToDanceGroup != null) return true;
                }
                else
                {
                    if (await connectionUserToDanceGroupService.update(connectionUserToDanceGroup)) return true;
                }
            }
            else if (status == 0)
            {
                if (await connectionUserToDanceGroupService.delete(danceGroup, user)) return true;
            }

            return false;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            ConnectionUserToDanceGroup connectionUserToDanceGroup = await connectionUserToDanceGroupService.findById(id);
            if (connectionUserToDanceGroup == null) return false;

            return await connectionUserToDanceGroupService.delete(connectionUserToDanceGroup);
        }
    }
}
