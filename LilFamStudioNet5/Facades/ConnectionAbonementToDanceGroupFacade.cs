using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionAbonementToDanceGroup;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class ConnectionAbonementToDanceGroupFacade
    {
        public ApplicationDbContext _dbc;
        public ConnectionAbonementToDanceGroupFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<int>> listAllConnectedIdOfAbonementsByIdOfDanceGroup(int id_of_dance_group)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(id_of_dance_group);
            if (danceGroup == null) return null;

            return await listAllConnectedIdOfAbonementsByDanceGroup(danceGroup);
        }

        public async Task<List<int>> listAllConnectedIdOfAbonementsByDanceGroup(DanceGroup danceGroup)
        {
            ConnectionAbonementToDanceGroupService connectionAbonementToDanceGroupService = new ConnectionAbonementToDanceGroupService(_dbc);
            List<ConnectionAbonementToDanceGroup> connectionAbonementToDanceGroups = await connectionAbonementToDanceGroupService.listAllByDanceGroup(danceGroup);
            List<int> listAllConnectedIdOfAbonementsByDanceGroup = new List<int>();
            foreach (ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup in connectionAbonementToDanceGroups)
            {
                if (connectionAbonementToDanceGroup.abonement == null) continue;
                listAllConnectedIdOfAbonementsByDanceGroup.Add(connectionAbonementToDanceGroup.abonement.id);
            }

            return listAllConnectedIdOfAbonementsByDanceGroup;
        }

        public async Task<HashSet<int>> hashAllConnectedIdOfAbonementsByDanceGroup(DanceGroup danceGroup)
        {
            ConnectionAbonementToDanceGroupService connectionAbonementToDanceGroupService = new ConnectionAbonementToDanceGroupService(_dbc);
            List<ConnectionAbonementToDanceGroup> connectionAbonementToDanceGroups = await connectionAbonementToDanceGroupService.listAllByDanceGroup(danceGroup);
            HashSet<int> hashAllConnectedIdOfAbonementsByDanceGroup = new HashSet<int>();
            foreach (ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup in connectionAbonementToDanceGroups)
            {
                if (connectionAbonementToDanceGroup.abonement == null) continue;
                hashAllConnectedIdOfAbonementsByDanceGroup.Add(connectionAbonementToDanceGroup.abonement.id);
            }

            return hashAllConnectedIdOfAbonementsByDanceGroup;
        }

        public async Task<HashSet<int>> hashAnyConnectedIdOfAbonementToAnyDanceGroup(User user, bool isForApp = false)
        {
            DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
            List<DanceGroup> danceGroupsForUser = await danceGroupFacade.listAllForUser(user, isForApp);

            HashSet<int> hashAnyConnectedIdOfAbonementByDanceGroupToUserAnswer = new HashSet<int>();
            HashSet<int> hashAllConnectedIdOfAbonementsByDanceGroupTMP = new HashSet<int>();
            foreach (DanceGroup danceGroup in danceGroupsForUser)
            {
                hashAllConnectedIdOfAbonementsByDanceGroupTMP = await hashAllConnectedIdOfAbonementsByDanceGroup(danceGroup);
                //System.Diagnostics.Debug.WriteLine("Рассматриваем группу " + danceGroup.name + " | число подключенных абонементов: " + hashAllConnectedIdOfAbonementsByDanceGroupTMP.Count());
                hashAnyConnectedIdOfAbonementByDanceGroupToUserAnswer.UnionWith(hashAllConnectedIdOfAbonementsByDanceGroupTMP);
                //System.Diagnostics.Debug.WriteLine("Промежуточное количество подключенных абонементов: " + hashAnyConnectedIdOfAbonementByDanceGroupToUserAnswer.Count());
            }
            return hashAnyConnectedIdOfAbonementByDanceGroupToUserAnswer;
        }

        public async Task<bool> update(ConnectionAbonementToDanceGroupUpdateDTO connectionAbonementToDanceGroupUpdateDTO)
        {
            AbonementService abonementService = new AbonementService(_dbc);
            Abonement abonement = await abonementService.findById(connectionAbonementToDanceGroupUpdateDTO.id_of_abonement);
            if (abonement == null) return false;

            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(connectionAbonementToDanceGroupUpdateDTO.id_of_dance_group);
            if (danceGroup == null) return false;

            ConnectionAbonementToDanceGroupService connectionAbonementToDanceGroupService = new ConnectionAbonementToDanceGroupService(_dbc);
            if (connectionAbonementToDanceGroupUpdateDTO.status == 1)
            {
                ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup = await connectionAbonementToDanceGroupService.add(abonement, danceGroup);
                if (connectionAbonementToDanceGroup != null) return true;
            } else if (connectionAbonementToDanceGroupUpdateDTO.status == 0)
            {
                if (await connectionAbonementToDanceGroupService.delete(abonement, danceGroup)) return true;
            }

            return false;
        }

        public async Task<bool> delete(int id)
        {
            ConnectionAbonementToDanceGroupService connectionAbonementToDanceGroupService = new ConnectionAbonementToDanceGroupService(_dbc);
            ConnectionAbonementToDanceGroup connectionAbonementToDanceGroup = await connectionAbonementToDanceGroupService.findById(id);
            if (connectionAbonementToDanceGroup == null) return false;

            return await connectionAbonementToDanceGroupService.delete(connectionAbonementToDanceGroup);
        }

    }
}
