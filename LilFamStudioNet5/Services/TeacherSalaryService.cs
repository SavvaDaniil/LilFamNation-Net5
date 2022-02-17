using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class TeacherSalaryService
    {
        public ApplicationDbContext _dbc;
        public TeacherSalaryService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<TeacherSalary> findById(int id)
        {
            return await _dbc.TeacherSalaries.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<TeacherSalary> find(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek, Teacher teacher)
        {
            return await _dbc.TeacherSalaries
                .FirstOrDefaultAsync(p => p.dateOfDay == dateOfDay && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek && p.teacher == teacher);
        }

        public async Task<TeacherSalary> find(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            return await _dbc.TeacherSalaries
                .Include(p => p.teacher)
                .FirstOrDefaultAsync(p => p.dateOfDay == dateOfDay && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek);
        }

        public async Task<List<TeacherSalary>> listAll()
        {
            return await _dbc.TeacherSalaries.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<TeacherSalary>> listAllByDanceGroupAndDates(DanceGroup danceGroup, DateTime dateFrom, DateTime dateTo)
        {
            return await _dbc.TeacherSalaries
                .Include(p => p.danceGroupDayOfWeek)
                .Where(p => p.danceGroup == danceGroup && p.dateOfDay >= dateFrom && p.dateOfDay <= dateTo)
                .OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<TeacherSalary> add(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek, Teacher teacher, int price)
        {
            TeacherSalary teacherSalary = new TeacherSalary();

            teacherSalary.dateOfAdd = dateOfDay;
            teacherSalary.dateOfDay = dateOfDay;
            teacherSalary.dateOfUpdate = dateOfDay;
            teacherSalary.danceGroup = danceGroup;
            teacherSalary.danceGroupDayOfWeek = danceGroupDayOfWeek;
            teacherSalary.teacher = teacher;
            teacherSalary.priceAuto = price;
            teacherSalary.priceFact = price;
            teacherSalary.status = 1;

            await _dbc.TeacherSalaries.AddAsync(teacherSalary);
            await _dbc.SaveChangesAsync();

            return teacherSalary;
        }

        public async Task<TeacherSalary> updateAuto(TeacherSalary teacherSalary, int price)
        {
            teacherSalary.priceAuto = price;
            teacherSalary.dateOfUpdate = DateTime.Now.Date;

            if(teacherSalary.isChangedByAdmin == 0) teacherSalary.priceFact = price;

            await _dbc.SaveChangesAsync();
            return teacherSalary;
        }

        public async Task<TeacherSalary> updateFact(TeacherSalary teacherSalary, int priceFact)
        {
            teacherSalary.dateOfUpdate = DateTime.Now.Date;
            teacherSalary.isChangedByAdmin = 1;
            teacherSalary.priceFact = priceFact;

            await _dbc.SaveChangesAsync();
            return teacherSalary;
        }

        public async Task<bool> setIsChangeByAdmin(TeacherSalary teacherSalary, int setIsChangeByAdmin)
        {
            if (setIsChangeByAdmin != 0 && setIsChangeByAdmin != 1) return false;
            if(setIsChangeByAdmin == 1)
            {
                teacherSalary.isChangedByAdmin = 1;
            } else
            {
                teacherSalary.priceFact = teacherSalary.priceAuto;
                teacherSalary.isChangedByAdmin = 0;
            }
            teacherSalary.dateOfUpdate = DateTime.Now.Date;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            TeacherSalary teacherSalary = await findById(id);
            if (teacherSalary == null) return false;
            _dbc.TeacherSalaries.Remove(teacherSalary);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(TeacherSalary teacherSalary)
        {
            _dbc.TeacherSalaries.Remove(teacherSalary);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
