using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.TeacherRate;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.TeacherRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class TeacherRateFacade
    {
        public ApplicationDbContext _dbc;
        public TeacherRateFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<TeacherRateLiteViewModel>> listAllLiteByTeacher(Teacher teacher)
        {
            TeacherRateService teacherRateService = new TeacherRateService(_dbc);
            List<TeacherRate> teacherRates = await teacherRateService.listAllByTeacher(teacher);
            List<TeacherRateLiteViewModel> teacherRateLiteViewModels = new List<TeacherRateLiteViewModel>();
            foreach (TeacherRate teacherRate in teacherRates)
            {
                teacherRateLiteViewModels.Add(
                    new TeacherRateLiteViewModel(
                        teacherRate.id,
                        teacherRate.special,
                        teacherRate.students,
                        teacherRate.howMuchMoney
                    )    
                );
            }
            return teacherRateLiteViewModels;
        }

        public async Task<JsonAnswerStatus> add(TeacherRateNewDTO teacherRateNewDTO)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            Teacher teacher = await teacherService.findById(teacherRateNewDTO.id_of_teacher);
            if (teacher == null) return new JsonAnswerStatus("error", "not_found_teacher");

            TeacherRateService teacherRateService = new TeacherRateService(_dbc);
            if (await teacherRateService.add(teacher) != null)
            {
                return new JsonAnswerStatus("success", null);
            }
            return new JsonAnswerStatus("error", "unknown");
        }

        public async Task<JsonAnswerStatus> update(TeacherRateDTO teacherRateDTO)
        {
            TeacherRateService teacherRateService = new TeacherRateService(_dbc);
            TeacherRate teacherRate = await teacherRateService.findById(teacherRateDTO.id_of_teacher_rate);
            if (teacherRate == null) return new JsonAnswerStatus("error", "not_found_teacher_rate");

            return (
                await teacherRateService.update(teacherRate, teacherRateDTO.name, teacherRateDTO.value)
                ? new JsonAnswerStatus("success", null)
                : new JsonAnswerStatus("error", "unknown")
            );
        }

        public async Task<JsonAnswerStatus> deleteByTeacherRateIdDTO(TeacherRateIdDTO teacherRateIdDTO)
        {
            TeacherRateService teacherRateService = new TeacherRateService(_dbc);
            TeacherRate teacherRate = await teacherRateService.findById(teacherRateIdDTO.id_of_teacher_rate);
            if (teacherRate == null) return new JsonAnswerStatus("error", "not_found_teacher_rate");

            if (await teacherRateService.delete(teacherRate))
            {
                return new JsonAnswerStatus("success", null);
            }
            return new JsonAnswerStatus("error", "unknown");
        }
    }
}
