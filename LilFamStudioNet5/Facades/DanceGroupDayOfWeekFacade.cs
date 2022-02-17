using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.DanceGroupDayOfWeek;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.DanceGroupDayOfWeek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class DanceGroupDayOfWeekFacade
    {
        public ApplicationDbContext _dbc;
        public DanceGroupDayOfWeekFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<DanceGroupDayOfWeekLiteViewModel>> listAllLiteByIdOfDanceGroup(int id_of_dance_group)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(id_of_dance_group);
            if (danceGroup == null) return null;

            return await listAllLiteByDanceGroup(danceGroup);
        }

        public async Task<List<DanceGroupDayOfWeekLiteViewModel>> listAllLiteByDanceGroup(DanceGroup danceGroup)
        {
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            List<DanceGroupDayOfWeek> danceGroupDayOfWeeks = await danceGroupDayOfWeekService.listAllByDanceGroup(danceGroup);
            List<DanceGroupDayOfWeekLiteViewModel> danceGroupDayOfWeekLiteViewModels = new List<DanceGroupDayOfWeekLiteViewModel>();
            foreach (DanceGroupDayOfWeek danceGroupDayOfWeek in danceGroupDayOfWeeks)
            {
                danceGroupDayOfWeekLiteViewModels.Add(
                    new DanceGroupDayOfWeekLiteViewModel(
                        danceGroupDayOfWeek.id,
                        danceGroupDayOfWeek.dayOfWeek,
                        getDayOfWeekByDateTimeDayOfWeek(danceGroupDayOfWeek.dayOfWeek),
                        danceGroupDayOfWeek.status,
                        danceGroupDayOfWeek.timeFrom,
                        danceGroupDayOfWeek.timeTo
                    )
                );
            }

            return danceGroupDayOfWeekLiteViewModels;
        }

        public async Task<LinkedList<int>> listAllDaysOfWeekOfDanceGroup(DanceGroup danceGroup)
        {
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            List<DanceGroupDayOfWeek> danceGroupDayOfWeeks = await danceGroupDayOfWeekService.listAllByDanceGroup(danceGroup);
            LinkedList<int> daysOfWeekOfDanceGroup = new LinkedList<int>();
            foreach (DanceGroupDayOfWeek danceGroupDayOfWeek in danceGroupDayOfWeeks)
            {
                if (!daysOfWeekOfDanceGroup.Contains(danceGroupDayOfWeek.dayOfWeek)) daysOfWeekOfDanceGroup.AddLast(danceGroupDayOfWeek.dayOfWeek);
            }
            return daysOfWeekOfDanceGroup;
        }

        public string getDayOfWeekByDateTimeDayOfWeek(int dayOfWeek)
        {
            if (dayOfWeek > 6) return null;
            string[] daysOfWeek = new string[] { "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };
            return daysOfWeek[dayOfWeek];
        }



        public async Task<DanceGroupDayOfWeek> add(DanceGroupDayOfWeekNewDTO danceGroupDayOfWeekNewDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(danceGroupDayOfWeekNewDTO.id_of_dance_group);
            if (danceGroup == null) return null;
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            DanceGroupDayOfWeek danceGroupDayOfWeek = await danceGroupDayOfWeekService.add(danceGroup);
            return danceGroupDayOfWeek;
        }

        public async Task<bool> delete(int id)
        {
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            DanceGroupDayOfWeek danceGroupDayOfWeek = await danceGroupDayOfWeekService.findById(id);
            if (danceGroupDayOfWeek == null) return false;

            return await danceGroupDayOfWeekService.delete(danceGroupDayOfWeek);
        }

        public async Task<JsonAnswerStatus> update(DanceGroupDayOfWeekEditByColumnDTO danceGroupDayOfWeekEditByColumnDTO)
        {
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            DanceGroupDayOfWeek danceGroupDayOfWeek = await danceGroupDayOfWeekService.findById(danceGroupDayOfWeekEditByColumnDTO.id_of_dance_group_day_of_week);
            if (danceGroupDayOfWeek == null) return new JsonAnswerStatus("error", "not_found");

            await danceGroupDayOfWeekService.updateByColumn(
                danceGroupDayOfWeek, 
                danceGroupDayOfWeekEditByColumnDTO.name,
                danceGroupDayOfWeekEditByColumnDTO.value,
                danceGroupDayOfWeekEditByColumnDTO.time
            );

            return new JsonAnswerStatus("success", null);
        }


        public string getTimeFromTimeTo(DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            if (danceGroupDayOfWeek.timeFrom == null || danceGroupDayOfWeek.timeTo == null) return null;
            return danceGroupDayOfWeek.timeFrom.Value.ToString(@"hh\:mm") + " - " + danceGroupDayOfWeek.timeTo.Value.ToString(@"hh\:mm");
        }

        public string getTimeFrom(DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            if (danceGroupDayOfWeek.timeFrom == null) return null;
            return danceGroupDayOfWeek.timeFrom.Value.ToString(@"hh\:mm");
        }

        public string getTimeTo(DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            if (danceGroupDayOfWeek.timeTo == null) return null;
            return danceGroupDayOfWeek.timeTo.Value.ToString(@"hh\:mm");
        }

    }
}
