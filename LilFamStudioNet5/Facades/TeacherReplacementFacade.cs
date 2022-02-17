using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.TeacherReplacement;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Teacher;
using LilFamStudioNet5.ViewModels.TeacherReplacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class TeacherReplacementFacade
    {
        public ApplicationDbContext _dbc;
        public TeacherReplacementFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<JsonAnswerStatus> getEdit(TeacherReplacementEditDTO teacherReplacementEditDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(teacherReplacementEditDTO.id_of_dance_group);
            if (danceGroup == null) return new JsonAnswerStatus("error", "not_found_dance_group");

            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            DanceGroupDayOfWeek danceGroupDayOfWeek = await danceGroupDayOfWeekService.findById(teacherReplacementEditDTO.id_of_dance_group_day_of_week);
            if (danceGroupDayOfWeek == null) return new JsonAnswerStatus("error", "not_found_dance_group_day_of_week");

            DateTime dateOfDay = DateTime.Now.Date;
            if(!DateTime.TryParse(teacherReplacementEditDTO.dateOfDay, out dateOfDay))return new JsonAnswerStatus("error", "not_date_of_lesson");

            Teacher teacherOfDanceGroup = (danceGroup.teacher != null ? danceGroup.teacher : null);

            Teacher teacherReplace = null;
            TeacherReplacementService teacherReplacementService = new TeacherReplacementService(_dbc);
            TeacherReplacement teacherReplacement = await teacherReplacementService.find(dateOfDay, danceGroup, danceGroupDayOfWeek);
            if(teacherReplacement != null)
            {
                if (teacherReplacement.teacherReplace != null) teacherReplace = teacherReplacement.teacherReplace;
            }

            //TeacherReplacementStatusViewModel
            TeacherService teacherService = new TeacherService(_dbc);
            List<Teacher> teachers = await teacherService.listAll();
            if(teacherOfDanceGroup != null) teachers.Remove(teacherOfDanceGroup);

            List<TeacherLiteViewModel> teacherLiteViewModels = new List<TeacherLiteViewModel>();
            foreach (Teacher teacher in teachers)
            {
                System.Diagnostics.Debug.WriteLine("teacher: " + teacher.id);
                teacherLiteViewModels.Add(new TeacherLiteViewModel(teacher.id, teacher.name));
            }

            DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);

            return new JsonAnswerStatus(
                "success", 
                null, 
                new TeacherReplacementStatusViewModel(
                    (teacherReplace != null ? teacherReplace.id : 0),
                    teacherLiteViewModels,
                    dateOfDay,
                    danceGroup.name,
                    danceGroupDayOfWeekFacade.getDayOfWeekByDateTimeDayOfWeek(danceGroupDayOfWeek.dayOfWeek) + " " + danceGroupDayOfWeekFacade.getTimeFromTimeTo(danceGroupDayOfWeek)
                )
            );
        }

        public async Task<bool> update(TeacherReplacementUpdateDTO teacherReplacementUpdateDTO)
        {
            DanceGroupService danceGroupService = new DanceGroupService(_dbc);
            DanceGroup danceGroup = await danceGroupService.findById(teacherReplacementUpdateDTO.id_of_dance_group);
            if (danceGroup == null) return false;

            DanceGroupDayOfWeekService danceGroupDayOfWeekService = new DanceGroupDayOfWeekService(_dbc);
            DanceGroupDayOfWeek danceGroupDayOfWeek = await danceGroupDayOfWeekService.findById(teacherReplacementUpdateDTO.id_of_dance_group_day_of_week);
            if (danceGroupDayOfWeek == null) return false;

            Teacher teacherReplace = null;
            int status = 0;
            if(teacherReplacementUpdateDTO.id_of_teacher_replace != 0)
            {
                status = 1;
                TeacherService teacherService = new TeacherService(_dbc);
                teacherReplace = await teacherService.findById(teacherReplacementUpdateDTO.id_of_teacher_replace);
                if (teacherReplace == null) return false;
            }
            //if (teacherReplace == null) return false;

            DateTime dateOfDay = DateTime.Now.Date;
            DateTime.TryParse(teacherReplacementUpdateDTO.dateOfDay, out dateOfDay);

            return await update(dateOfDay, danceGroup, danceGroupDayOfWeek, teacherReplace, status);
        }

        public async Task<bool> update(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek, Teacher teacherReplace, int status)
        {
            TeacherReplacementService teacherReplacementService = new TeacherReplacementService(_dbc);
            if (status == 1)
            {
                if (teacherReplace == null) return false;
                TeacherReplacement teacherReplacement = await teacherReplacementService.find(dateOfDay, danceGroup, danceGroupDayOfWeek);
                if (teacherReplacement == null)
                {
                    teacherReplacement = await teacherReplacementService.add(dateOfDay, danceGroup, danceGroupDayOfWeek, teacherReplace);
                    if (teacherReplacement != null)
                    {
                        //нужен перерасчет ззарплаты

                        return true;
                    }
                }
                else
                {
                    if (await teacherReplacementService.update(teacherReplacement, teacherReplace))
                    {
                        //нужен перерасчет ззарплаты

                        return true;
                    }
                }
            }
            else if (status == 0)
            {
                if (await teacherReplacementService.delete(dateOfDay, danceGroup, danceGroupDayOfWeek))
                {
                    //нужен перерасчет ззарплаты

                    return true;
                }
            }

            return false;
        }


        public async Task<bool> delete(int id)
        {
            TeacherReplacementService TeacherReplacementService = new TeacherReplacementService(_dbc);
            TeacherReplacement TeacherReplacement = await TeacherReplacementService.findById(id);
            if (TeacherReplacement == null) return false;

            return await TeacherReplacementService.delete(TeacherReplacement);
        }
    }
}
