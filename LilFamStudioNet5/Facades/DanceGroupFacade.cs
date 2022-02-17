using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.DanceGroup;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.Branch;
using LilFamStudioNet5.ViewModels.DanceGroup;
using LilFamStudioNet5.ViewModels.DanceGroupDayOfWeek;
using LilFamStudioNet5.ViewModels.Teacher;
using LilFamStudioNet5.ViewModels.Visit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class DanceGroupFacade
    {
        public ApplicationDbContext _dbc;
        public DanceGroupFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<DanceGroupLiteViewModel>> listAllLite()
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = await danceGroupService.listAll();
            List<DanceGroupLiteViewModel> danceGroupLiteViewModels = new List<DanceGroupLiteViewModel>();
            foreach (DanceGroup danceGroup in danceGroups)
            {
                danceGroupLiteViewModels.Add(new DanceGroupLiteViewModel(
                        danceGroup.id,
                        danceGroup.name,
                        (danceGroup.teacher != null ? danceGroup.teacher.name : "- преподаватель не установлен -"),
                        danceGroup.status,
                        danceGroup.statusOfApp
                    )
                );
            }

            return danceGroupLiteViewModels;
        }


        public async Task<List<DanceGroupByDanceGroupDayOfWeekLiteViewModel>> listAllByDanceGroupDayOfWeek()
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = await danceGroupService.listAllByName();
            List<DanceGroupByDanceGroupDayOfWeekLiteViewModel> danceGroupByDanceGroupDayOfWeekLiteViewModels = new List<DanceGroupByDanceGroupDayOfWeekLiteViewModel>();

            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            List<DanceGroupDayOfWeek> danceGroupDayOfWeeks = await danceGroupDayOfWeekService.listAllOrderByTimeFrom();

            foreach (DanceGroup danceGroup in danceGroups)
            {
                foreach (DanceGroupDayOfWeek danceGroupDayOfWeek in danceGroupDayOfWeeks)
                {
                    if(danceGroupDayOfWeek.danceGroup == danceGroup)
                    {
                        danceGroupByDanceGroupDayOfWeekLiteViewModels.Add(
                            new DanceGroupByDanceGroupDayOfWeekLiteViewModel(
                                danceGroup.id,
                                danceGroupDayOfWeek.id,
                                danceGroup.id + " - " + danceGroup.name + " | " 
                                + danceGroupDayOfWeekFacade.getDayOfWeekByDateTimeDayOfWeek(danceGroupDayOfWeek.dayOfWeek)
                                + " " + danceGroupDayOfWeekFacade.getTimeFromTimeTo(danceGroupDayOfWeek)
                            )    
                        );
                    }
                }
            }

            return danceGroupByDanceGroupDayOfWeekLiteViewModels;
        }


        public async Task<List<DanceGroupConnectionToUserAdminViewModel>> listAllWithConnectectionToUserAdmin(int id_of_user)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return null;

            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = await danceGroupService.listAll();
            List<DanceGroupConnectionToUserAdminViewModel> danceGroupConnectionToUserAdminViewModels = new List<DanceGroupConnectionToUserAdminViewModel>();

            ConnectionDanceGroupToUserAdminFacade connectionDanceGroupToUserAdminFacade = new ConnectionDanceGroupToUserAdminFacade(_dbc);
            List<int> listAllConnectedIdOfDanceGroupsByUser = await connectionDanceGroupToUserAdminFacade.listAllConnectedIdOfDanceGroupsByUser(user);

            foreach (DanceGroup danceGroup in danceGroups)
            {
                danceGroupConnectionToUserAdminViewModels.Add(
                    new DanceGroupConnectionToUserAdminViewModel(
                        danceGroup.id,
                        danceGroup.name,
                        (listAllConnectedIdOfDanceGroupsByUser.Contains(danceGroup.id) ? 1 : 0)
                    )
                );
            }

            return danceGroupConnectionToUserAdminViewModels;
        }


        public async Task<DanceGroupEditViewModel> getEdit(int id)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(id);
            if (danceGroup == null) return null;

            TeacherFacade teacherFacade = new TeacherFacade(_dbc);
            TeacherLiteViewModel teacherLiteViewModel = null;
            if (danceGroup.teacher != null)
            {
                teacherLiteViewModel = await teacherFacade.getLiteBydId(danceGroup.teacher.id);
            }
            List<TeacherLiteViewModel> teacherLiteViewModels = await teacherFacade.listAllLite(true);

            BranchFacade branchFacade = new BranchFacade(_dbc);
            BranchLiteViewModel branchLiteViewModel = null;
            if (danceGroup.branch != null)
            {
                branchLiteViewModel = await branchFacade.getLiteBydId(danceGroup.branch.id);
            }
            List<BranchLiteViewModel> branchLiteViewModels = await branchFacade.listAllLite();

            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
            List<DanceGroupDayOfWeekLiteViewModel> danceGroupDayOfWeekLiteViewModels = await danceGroupDayOfWeekFacade.listAllLiteByDanceGroup(danceGroup);

            AbonementFacade abonementFacade = new AbonementFacade(_dbc);
            List<AbonementLiteViewModel> abonementLiteViewModels = await abonementFacade.listAllLite(true);

            ConnectionAbonementToDanceGroupFacade connectionAbonementToDanceGroupFacade = new ConnectionAbonementToDanceGroupFacade(_dbc);
            List<int> listIdOfConnectedAbonementsToDanceGroup = await connectionAbonementToDanceGroupFacade.listAllConnectedIdOfAbonementsByDanceGroup(danceGroup);

            DanceGroupEditViewModel danceGroupEditViewModel = new DanceGroupEditViewModel(
                danceGroup.id,
                danceGroup.name,
                teacherLiteViewModel,
                teacherLiteViewModels,
                danceGroup.description,
                branchLiteViewModel,
                branchLiteViewModels,
                danceGroup.status,
                danceGroup.isEvent,
                danceGroup.statusOfApp,
                danceGroup.statusOfCreative,
                danceGroup.isActiveReservation,
                danceGroup.isAbonementsAllowAll,
                danceGroupDayOfWeekLiteViewModels,
                abonementLiteViewModels,
                listIdOfConnectedAbonementsToDanceGroup
            );

            return danceGroupEditViewModel;
        }



        public async Task<DanceGroupScheduleWithNameOfDayOfWeek> getScheduleByDate(string date_of_day)
        {
            DateTime dateOfDay = DateTime.Now;
            if (date_of_day != null && date_of_day != "" && date_of_day != "null") DateTime.TryParse(date_of_day, out dateOfDay);
            dateOfDay = dateOfDay.Date;
            int dayOfWeek = (int)dateOfDay.DayOfWeek;

            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = await danceGroupService.listAll();

            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            List<DanceGroupDayOfWeek> danceGroupDayOfWeeks = await danceGroupDayOfWeekService.listAll();

            TeacherReplacementService teacherReplacementService = new TeacherReplacementService(_dbc);
            List<TeacherReplacement> teacherReplacementsByDates = await teacherReplacementService.listAllByDates(dateOfDay, dateOfDay);

            bool isReplacementExistForLesson = false;

            List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels = new List<DanceGroupScheduleViewModel>();
            foreach (DanceGroup danceGroup in danceGroups) {
                foreach (DanceGroupDayOfWeek danceGroupDayOfWeek in danceGroupDayOfWeeks)
                {
                    if(danceGroup == danceGroupDayOfWeek.danceGroup && danceGroupDayOfWeek.dayOfWeek == dayOfWeek)
                    {
                        isReplacementExistForLesson = teacherReplacementsByDates
                                .Where(p => p.dateOfDay == dateOfDay && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek)
                                .Any();
                        danceGroupScheduleViewModels.Add(
                            new DanceGroupScheduleViewModel(
                                danceGroup.id,
                                danceGroup.name,
                                danceGroupDayOfWeek.id,
                                dayOfWeek,
                                (danceGroupDayOfWeek.timeFrom != null ? danceGroupDayOfWeek.timeFrom.Value.ToString(@"hh\:mm") : null),
                                (danceGroupDayOfWeek.timeTo != null ? danceGroupDayOfWeek.timeTo.Value.ToString(@"hh\:mm") : null),
                                date_of_day,
                                (danceGroup.teacher != null ? danceGroup.teacher.id : 0),
                                (danceGroup.teacher != null ? danceGroup.teacher.name : null),
                                (danceGroup.branch != null ? danceGroup.branch.id : 0),
                                (danceGroup.branch != null ? danceGroup.branch.name : null),
                                danceGroup.statusOfCreative,
                                danceGroup.isActiveReservation,
                                0,
                                isReplacementExistForLesson
                            )
                        );
                    }
                }
            }

            danceGroupScheduleViewModels.Sort((x, y) => x.time_from.CompareTo(y.time_from));

            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);

            return new DanceGroupScheduleWithNameOfDayOfWeek(
                danceGroupDayOfWeekFacade.getDayOfWeekByDateTimeDayOfWeek(dayOfWeek),
                danceGroupScheduleViewModels
            );
        }

        public async Task<List<DanceGroupScheduleWithNameOfDayOfWeek>> getScheduleFromDateToDate(HttpContext httpContext, string filterDateFromStr, string filterDateToStr, bool isForApp = false)
        {
            //это перехват пользователя насчет творческих групп
            UserFacade userFacade = new UserFacade(_dbc);
            User user = await userFacade.getCurrentOrNull(httpContext);
            List<int> idsOfAllConnectedDanceGropusToUser = new List<int>();
            if (user != null) {
                ConnectionUserToDanceGroupFacade connectionUserToDanceGroupFacade = new ConnectionUserToDanceGroupFacade(_dbc);
                idsOfAllConnectedDanceGropusToUser = await connectionUserToDanceGroupFacade.listAllConnectedIdOfDanceGroupsByUser(user);
            }


            DateTime filterDateFrom = DateTime.Now;
            if (filterDateToStr != null && filterDateToStr != "" && filterDateToStr != "null") DateTime.TryParse(filterDateFromStr, out filterDateFrom);
            filterDateFrom = filterDateFrom.Date;

            DateTime filterDateTo = DateTime.Now;
            if (filterDateToStr != null && filterDateToStr != "" && filterDateToStr != "null") DateTime.TryParse(filterDateToStr, out filterDateTo);
            filterDateTo = filterDateTo.Date;

            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = (isForApp ? await danceGroupService.listAllActiveForApp() : await danceGroupService.listAll());

            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            List<DanceGroupDayOfWeek> danceGroupDayOfWeeks = await danceGroupDayOfWeekService.listAll();

            TeacherReplacementService teacherReplacementService = new TeacherReplacementService(_dbc);
            List<TeacherReplacement> teacherReplacementsByDates = await teacherReplacementService.listAllByDates(filterDateFrom, filterDateTo);


            List<DanceGroupScheduleWithNameOfDayOfWeek> danceGroupScheduleWithNameOfDayOfWeeks = new List<DanceGroupScheduleWithNameOfDayOfWeek>();
            DateTime dateOfLessons = filterDateFrom;
            string nameOfDayInSchedule = null;
            List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels = new List<DanceGroupScheduleViewModel>();
            bool isReplacementExistForLesson = false;
            while (dateOfLessons <= filterDateTo)
            {
                System.Diagnostics.Debug.WriteLine("dateOfLessons: "+ dateOfLessons.ToString("dd.MM.yyyy"));
                nameOfDayInSchedule = danceGroupDayOfWeekFacade.getDayOfWeekByDateTimeDayOfWeek((int)dateOfLessons.DayOfWeek)
                    + " / " + dateOfLessons.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU"))
                    + " " + dateOfLessons.ToString("dd");
                danceGroupScheduleViewModels = new List<DanceGroupScheduleViewModel>();

                foreach (DanceGroup danceGroup in danceGroups)
                {
                    //проверка на творческую или закрытую
                    if(danceGroup.statusOfCreative == 1)
                    {
                        if (!idsOfAllConnectedDanceGropusToUser.Contains(danceGroup.id)) continue;
                    }

                    foreach (DanceGroupDayOfWeek danceGroupDayOfWeek in danceGroupDayOfWeeks)
                    {
                        if (danceGroupDayOfWeek.danceGroup != danceGroup) continue;
                        isReplacementExistForLesson = false;
                        if (danceGroupDayOfWeek.dayOfWeek == (int)dateOfLessons.DayOfWeek)
                        {

                            isReplacementExistForLesson = teacherReplacementsByDates
                                .Where(p => p.dateOfDay == dateOfLessons && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek)
                                .Any();
                            danceGroupScheduleViewModels.Add(
                                new DanceGroupScheduleViewModel(
                                    danceGroup.id,
                                    danceGroup.name,
                                    danceGroupDayOfWeek.id,
                                    danceGroupDayOfWeek.dayOfWeek,
                                    danceGroupDayOfWeekFacade.getTimeFrom(danceGroupDayOfWeek),
                                    danceGroupDayOfWeekFacade.getTimeFromTimeTo(danceGroupDayOfWeek),
                                    dateOfLessons.ToString("yyyy.MM.dd"),
                                    (danceGroup.teacher != null ? danceGroup.teacher.id : 0),
                                    (danceGroup.teacher != null ? danceGroup.teacher.name : null),
                                    (danceGroup.branch != null ? danceGroup.branch.id : 0),
                                    (danceGroup.branch != null ? danceGroup.branch.name : null),
                                    danceGroup.statusOfCreative,
                                    danceGroup.isActiveReservation,
                                    0,
                                    isReplacementExistForLesson
                                )
                            );
                        }
                    }
                }
                danceGroupScheduleWithNameOfDayOfWeeks.Add(
                    new DanceGroupScheduleWithNameOfDayOfWeek(
                        nameOfDayInSchedule,
                        danceGroupScheduleViewModels
                    )
                );

                dateOfLessons = dateOfLessons.AddDays(1);
            }

            return danceGroupScheduleWithNameOfDayOfWeeks;
        }

        public async Task<List<DanceGroupScheduleViewModel>> getScheduleForAppUserAdmin(HttpContext httpContext)
        {
            UserFacade userFacade = new UserFacade(_dbc);
            User user = await userFacade.getCurrentOrNull(httpContext);
            if (user == null) return null;
            if (user.statusOfAdmin != 1) return null;

            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = await danceGroupService.listAll();
            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            List<DanceGroupDayOfWeek> danceGroupDayOfWeeks = await danceGroupDayOfWeekService.listAll();

            DateTime dateOfLesson = DateTime.Now.Date.AddDays(-3);
            DateTime filterDateTo = DateTime.Now.Date.AddDays(3);
            ConnectionDanceGroupToUserAdminFacade connectionDanceGroupToUserAdminFacade = new ConnectionDanceGroupToUserAdminFacade(_dbc);
            HashSet<int> hashOfAllConnectedIdOfDanceGroupToUserAdmin = await connectionDanceGroupToUserAdminFacade.hashOfAllConnectedIdOfDanceGroupsByUser(user);

            TeacherReplacementService teacherReplacementService = new TeacherReplacementService(_dbc);
            List<TeacherReplacement> teacherReplacementsByDates = await teacherReplacementService.listAllByDates(dateOfLesson, filterDateTo);

            //очищаем от неподключенных групп
            danceGroups = danceGroups.Where(p => hashOfAllConnectedIdOfDanceGroupToUserAdmin.Contains(p.id)).ToList();
            if (danceGroups.Count == 0) return null;

            bool isReplacementExistForLesson = false;
            List<DanceGroupScheduleViewModel> danceGroupScheduleViewModels = new List<DanceGroupScheduleViewModel>();
            while (dateOfLesson <= filterDateTo)
            {
                foreach (DanceGroup danceGroup in danceGroups)
                {
                    foreach (DanceGroupDayOfWeek danceGroupDayOfWeek in danceGroupDayOfWeeks)
                    {
                        if (danceGroupDayOfWeek.danceGroup != danceGroup) continue;
                        isReplacementExistForLesson = false;
                        if (danceGroupDayOfWeek.dayOfWeek == (int)dateOfLesson.DayOfWeek)
                        {
                            isReplacementExistForLesson = teacherReplacementsByDates
                                .Where(p => p.dateOfDay == dateOfLesson && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek)
                                .Any();
                            danceGroupScheduleViewModels.Add(
                                new DanceGroupScheduleViewModel(
                                    danceGroup.id,
                                    danceGroup.name,
                                    danceGroupDayOfWeek.id,
                                    danceGroupDayOfWeek.dayOfWeek,
                                    danceGroupDayOfWeekFacade.getTimeFrom(danceGroupDayOfWeek),
                                    danceGroupDayOfWeekFacade.getTimeFromTimeTo(danceGroupDayOfWeek),
                                    dateOfLesson.ToString("yyyy.MM.dd"),
                                    (danceGroup.teacher != null ? danceGroup.teacher.id : 0),
                                    (danceGroup.teacher != null ? danceGroup.teacher.name : null),
                                    (danceGroup.branch != null ? danceGroup.branch.id : 0),
                                    (danceGroup.branch != null ? danceGroup.branch.name : null),
                                    danceGroup.statusOfCreative,
                                    danceGroup.isActiveReservation,
                                    0,
                                    isReplacementExistForLesson
                                )
                            );
                        }
                    }
                }
                dateOfLesson = dateOfLesson.AddDays(1);
            }
            return danceGroupScheduleViewModels;
        }



        public async Task<JsonAnswerStatus> getForApp(HttpContext httpContext, int id_of_dance_group, int id_of_dance_group_day_of_week, string date_of_lesson_str, int id_of_visit = 0)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(id_of_dance_group);
            if (danceGroup == null) return new JsonAnswerStatus("error", "not_found_dance_group");

            DateTime dateOfLesson = DateTime.Now;
            if (date_of_lesson_str != null && date_of_lesson_str != "" && date_of_lesson_str != "null") DateTime.TryParse(date_of_lesson_str, out dateOfLesson);
            dateOfLesson = dateOfLesson.Date;

            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            DanceGroupDayOfWeek danceGroupDayOfWeek = await danceGroupDayOfWeekService.findById(id_of_dance_group_day_of_week);
            if (danceGroupDayOfWeek == null) return new JsonAnswerStatus("error", "not_found_dance_group_day_of_week");
            if (danceGroupDayOfWeek.danceGroup == null) return new JsonAnswerStatus("error", "not_dance_group_in_dance_group_day_of_week");
            if (danceGroupDayOfWeek.danceGroup != danceGroup) return new JsonAnswerStatus("error", "not_found_dance_group_day_of_week");
            if (danceGroupDayOfWeek.dayOfWeek != (int)dateOfLesson.DayOfWeek) return new JsonAnswerStatus("error", "not_match_day_of_week");


            TeacherReplacementService teacherReplacementService = new TeacherReplacementService(_dbc);
            TeacherReplacement teacherReplacement = await teacherReplacementService.find(dateOfLesson, danceGroup, danceGroupDayOfWeek);
            bool isReplacementExistForLesson = (teacherReplacement != null);

            TeacherFacade teacherFacade = new TeacherFacade(_dbc);
            string teacherPosterSrc = null;
            if(teacherReplacement != null)
            {
                teacherPosterSrc = teacherFacade.getPosterSrc(teacherReplacement.id);
            } else if(danceGroup.teacher != null)
            {
                teacherPosterSrc = teacherFacade.getPosterSrc(danceGroup.teacher.id);
            }

            List<VisitLiteViewModel> visitLiteViewModels = new List<VisitLiteViewModel>();

            if(httpContext != null)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                User user = await userFacade.getCurrentOrNull(httpContext);
                if(user != null)
                {
                    VisitFacade visitFacade = new VisitFacade(_dbc);
                    if (id_of_visit != 0)
                    {
                        VisitLiteViewModel visitLiteViewModel = await visitFacade.getLiteById(id_of_visit);
                        if(visitLiteViewModel != null)
                        {
                            visitLiteViewModels.Add(visitLiteViewModel);
                        } else
                        {
                            visitLiteViewModels = await visitFacade.listAllByUserDanceGroupAndDate(user, danceGroup, danceGroupDayOfWeek, dateOfLesson);
                        }
                    }
                    else
                    {
                        visitLiteViewModels = await visitFacade.listAllByUserDanceGroupAndDate(user, danceGroup, danceGroupDayOfWeek, dateOfLesson);
                    }
                }
            }


            return new JsonAnswerStatus("success", null,
                new DanceGroupScheduleWithNameOfDayOfWeek(
                    danceGroupDayOfWeekFacade.getDayOfWeekByDateTimeDayOfWeek((int)dateOfLesson.DayOfWeek)
                        + " / " + dateOfLesson.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU"))
                        + " " + dateOfLesson.ToString("dd")
                        ,
                    new DanceGroupScheduleViewModel(
                        danceGroup.id,
                        danceGroup.name,
                        danceGroupDayOfWeek.id,
                        danceGroupDayOfWeek.dayOfWeek,
                        danceGroupDayOfWeekFacade.getTimeFrom(danceGroupDayOfWeek),
                        danceGroupDayOfWeekFacade.getTimeFromTimeTo(danceGroupDayOfWeek),
                        dateOfLesson.ToString("yyyy.MM.dd"),
                        (danceGroup.teacher != null ? danceGroup.teacher.id : 0),
                        (danceGroup.teacher != null ? danceGroup.teacher.name : null),
                        (danceGroup.branch != null ? danceGroup.branch.id : 0),
                        (danceGroup.branch != null ? danceGroup.branch.name : null),
                        danceGroup.statusOfCreative,
                        danceGroup.isActiveReservation,
                        0,
                        isReplacementExistForLesson,
                        danceGroup.description,
                        (danceGroup.branch != null ? danceGroup.branch.coordinates : null),
                        (teacherReplacement != null ? teacherReplacement.teacherReplace != null ? teacherReplacement.teacherReplace.id : 0 : 0),
                        (teacherReplacement != null ? teacherReplacement.teacherReplace != null ? teacherReplacement.teacherReplace.name : null : null),
                        teacherPosterSrc
                    )
                ),
                visitLiteViewModels
            );
        }




        public async Task<DanceGroup> add(DanceGroupNewDTO danceGroupNewDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            if (danceGroupNewDTO.name == null) return null;
            DanceGroup danceGroup = await danceGroupService.add(danceGroupNewDTO.name);
            return danceGroup;
        }

        public async Task<bool> delete(int id)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(id);
            if (danceGroup == null) return false;

            return await danceGroupService.delete(danceGroup);
        }

        public async Task<List<DanceGroup>> listAllForUser(User user, bool isForApp = false)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            List<DanceGroup> danceGroups = (isForApp ? await danceGroupService.listAllActiveForApp() : await danceGroupService.listAllActive());
            ConnectionUserToDanceGroupService connectionUserToDanceGroupService = new ConnectionUserToDanceGroupService(_dbc);
            List<DanceGroup> danceGroupsAnswer = new List<DanceGroup>();
            foreach (DanceGroup danceGroup in danceGroups)
            {
                if(danceGroup.statusOfCreative == 1)
                {
                    if(await connectionUserToDanceGroupService.isAnyDanceGroupAndUser(user, danceGroup)) if (!danceGroupsAnswer.Contains(danceGroup)) danceGroupsAnswer.Add(danceGroup);
                } else
                {
                    if (!danceGroupsAnswer.Contains(danceGroup)) danceGroupsAnswer.Add(danceGroup);
                }
            }
            return danceGroupsAnswer;
        }


        public async Task<JsonAnswerStatus> update(DanceGroupEditByColumnDTO danceGroupEditByColumnDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(danceGroupEditByColumnDTO.id_of_dance_group);
            if (danceGroup == null) return new JsonAnswerStatus("error", "not_found");

            Teacher teacher = null;
            if (danceGroupEditByColumnDTO.name == "teacher")
            {
                TeacherService teacherService = new TeacherService(_dbc);
                teacher = await teacherService.findById(int.Parse(danceGroupEditByColumnDTO.value));
            }
            Branch branch = null;
            if (danceGroupEditByColumnDTO.name == "branch")
            {
                BranchService branchService = new BranchService(_dbc);
                branch = await branchService.findById(int.Parse(danceGroupEditByColumnDTO.value));
            }

            await danceGroupService.updateByColumn(danceGroup, danceGroupEditByColumnDTO.name, danceGroupEditByColumnDTO.value, teacher, branch);

            return new JsonAnswerStatus("success", null);
        }

    }
}
