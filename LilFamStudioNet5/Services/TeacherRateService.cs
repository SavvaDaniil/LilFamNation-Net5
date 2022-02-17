using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class TeacherRateService
    {
        public ApplicationDbContext _dbc;
        public TeacherRateService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<TeacherRate> findById(int id)
        {
            return await _dbc.TeacherRates.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<TeacherRate>> listAll()
        {
            return await _dbc.TeacherRates.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<TeacherRate>> listAllByTeacher(Teacher teacher)
        {
            return await _dbc.TeacherRates
                .Where(p => p.teacher == teacher)
                .OrderBy(p => p.students)
                .ToListAsync();
        }

        public async Task<TeacherRate> add(Teacher teacher)
        {
            TeacherRate teacherRate = new TeacherRate();
            teacherRate.teacher = teacher;

            await _dbc.TeacherRates.AddAsync(teacherRate);
            await _dbc.SaveChangesAsync();

            return teacherRate;
        }

        public async Task<bool> update(TeacherRate teacherRate, string name, int value)
        {
            if (value < 0) value = 0;
            if (name == "students")
            {
                teacherRate.students = value;
            } else if (name == "how_much_money")
            {
                teacherRate.howMuchMoney = value;
            }
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            TeacherRate teacherRate = await findById(id);
            if (teacherRate == null) return false;
            _dbc.TeacherRates.Remove(teacherRate);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(TeacherRate teacherRate)
        {
            _dbc.TeacherRates.Remove(teacherRate);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteAllByTeacher(Teacher teacher)
        {
            List<TeacherRate> teacherRates = await listAllByTeacher(teacher);
            foreach (TeacherRate teacherRate in teacherRates)
            {
                _dbc.TeacherRates.Remove(teacherRate);
                await _dbc.SaveChangesAsync();
            }
            return true;
        }
    }
}
