using LilFamStudioNet5.Components;
using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Teacher;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Teacher;
using LilFamStudioNet5.ViewModels.TeacherRate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class TeacherFacade
    {
        public ApplicationDbContext _dbc;
        public TeacherFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<List<TeacherLiteViewModel>> listAllLite(bool isOrderByName = false)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            List<Teacher> teachers = (isOrderByName ? await teacherService.listAllOrderByName() : await teacherService.listAll());
            List<TeacherLiteViewModel> teacherLiteViewModels = new List<TeacherLiteViewModel>();
            foreach (Teacher teacher in teachers)
            {
                teacherLiteViewModels.Add(
                    new TeacherLiteViewModel(
                        teacher.id,
                        teacher.name,
                        this.getPosterSrc(teacher.id),
                        false
                    )
                );
            }

            return teacherLiteViewModels;
        }


        public async Task<TeacherEditViewModel> getEdit(int id)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            Teacher teacher = await teacherService.findById(id);
            if (teacher == null) return null;

            TeacherRateFacade teacherRateFacade = new TeacherRateFacade(_dbc);
            List<TeacherRateLiteViewModel> teacherRateLiteViewModels = await teacherRateFacade.listAllLiteByTeacher(teacher);

            TeacherEditViewModel teacherEditViewModel = new TeacherEditViewModel(
                teacher.id,
                teacher.name,
                this.getPosterSrc(teacher.id),
                teacher.stavka,
                teacher.stavkaPlus,
                teacherRateLiteViewModels,
                teacher.minStudents,
                teacher.raz,
                teacher.usual,
                teacher.unlim,
                teacher.procent,
                teacher.plusAfterStudents,
                teacher.plusAfterSumma
            );

            return teacherEditViewModel;
        }

        public async Task<Teacher> add(TeacherNewDTO teacherNewDTO)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            if (teacherNewDTO.name == null) return null;
            Teacher teacher = await teacherService.add(teacherNewDTO.name);

            return teacher;
        }

        public async Task<TeacherLiteViewModel> getLiteBydId(int id)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            Teacher teacher = await teacherService.findById(id);
            if (teacher == null) return null;
            return new TeacherLiteViewModel(teacher.id, teacher.name, getPosterSrc(teacher.id));
        }

        public async Task<bool> delete(int id)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            Teacher teacher = await teacherService.findById(id);
            if (teacher == null) return false;

            TeacherRateService teacherRateService = new TeacherRateService(_dbc);
            await teacherRateService.deleteAllByTeacher(teacher);

            return await teacherService.delete(teacher);
        }

        public async Task<JsonAnswerStatus> update(TeacherByColumnDTO teacherByColumnDTO)
        {
            TeacherService teacherService = new TeacherService(_dbc);
            Teacher teacher = await teacherService.findById(teacherByColumnDTO.id_of_teacher);
            if (teacher == null) return new JsonAnswerStatus("error", "not_found");

            if (teacherByColumnDTO.name == "poster" && teacherByColumnDTO.file != null)
            {
                if(!await uploadPoster(teacherByColumnDTO.file, teacher.id.ToString()))
                {
                    return new JsonAnswerStatus("error","cant_save_file");
                }
            } else if (teacherByColumnDTO.name == "posterDelete")
            {
                this.deletePoster(teacher.id);
            } else
            {
                await teacherService.updateByColumn(teacher, teacherByColumnDTO.name, teacherByColumnDTO.value);
            }

            return new JsonAnswerStatus("success", null);
        }

        private async Task<bool> uploadPoster(IFormFile file, string nameOfFile)
        {
            string path = Directory.GetCurrentDirectory();
            string uploadsForFiles = path + "\\wwwroot\\uploads\\teacher";
            if (!Directory.Exists(uploadsForFiles))
            {
                Directory.CreateDirectory(uploadsForFiles);
            }

            string pathwithfileName = uploadsForFiles + "\\" + nameOfFile + ".jpg";
            string pathwithTmpName = uploadsForFiles + "\\tmp.png";
            using (var fileStream = new FileStream(pathwithTmpName, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);

                fileStream.Dispose();
            }

            ResizeImageComponent resizeImageComponent = new ResizeImageComponent();
            resizeImageComponent.ResizeTmpImageAndSaveFinally_box(pathwithTmpName, pathwithfileName);

            if (File.Exists(pathwithTmpName)) File.Delete(pathwithTmpName);

            return true;
        }


        public string getPosterSrc(int id)
        {
            string folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\uploads\\teacher\\" + id.ToString() + ".jpg";
            if (File.Exists(folderPath))
            {
                return "/uploads/teacher/" + id.ToString() + ".jpg";
            }
            return null;
        }

        private void deletePoster(int id)
        {
            string path = Directory.GetCurrentDirectory();
            string filePath = path + "\\wwwroot\\uploads\\teacher\\" + id.ToString() + ".jpg";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
