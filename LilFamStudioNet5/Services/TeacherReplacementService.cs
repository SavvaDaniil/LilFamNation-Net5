using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class TeacherReplacementService
    {
        public ApplicationDbContext _dbc;
        public TeacherReplacementService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<TeacherReplacement> findById(int id)
        {
            return await _dbc.TeacherReplacements
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<TeacherReplacement> find(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            return await _dbc.TeacherReplacements
                .Include(p => p.teacherReplace)
                .FirstOrDefaultAsync(p => p.dateOfDay == dateOfDay && p.danceGroup == danceGroup && p.danceGroupDayOfWeek == danceGroupDayOfWeek);
        }

        public async Task<List<TeacherReplacement>> listAllByDanceGroupAndDates(DanceGroup danceGroup, DateTime dateFrom, DateTime dateTo)
        {
            return await _dbc.TeacherReplacements
                .Where(p => p.danceGroup == danceGroup && p.dateOfDay >= dateFrom && p.dateOfDay <= dateTo)
                .OrderByDescending(p => p.dateOfDay)
                .ToListAsync();

        }

        public async Task<List<TeacherReplacement>> listAllByDates(DateTime dateFrom, DateTime dateTo)
        {
            return await _dbc.TeacherReplacements
                .Where(p => p.dateOfDay >= dateFrom && p.dateOfDay <= dateTo)
                .OrderByDescending(p => p.dateOfDay)
                .ToListAsync();
        }

        public async Task<TeacherReplacement> add(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek, Teacher newTeacherReplace)
        {
            TeacherReplacement teacherReplacement = await find(dateOfDay, danceGroup, danceGroupDayOfWeek);
            if (teacherReplacement != null) return teacherReplacement;

            teacherReplacement = new TeacherReplacement();
            teacherReplacement.danceGroup = danceGroup;
            teacherReplacement.danceGroupDayOfWeek = danceGroupDayOfWeek;
            teacherReplacement.teacherReplace = newTeacherReplace;
            teacherReplacement.dateOfDay = dateOfDay;
            teacherReplacement.dateOfAdd = DateTime.Now;
            teacherReplacement.dateOfUpdate = DateTime.Now;

            await _dbc.TeacherReplacements.AddAsync(teacherReplacement);
            await _dbc.SaveChangesAsync();

            return teacherReplacement;
        }

        public async Task<bool> update(TeacherReplacement teacherReplacement, Teacher newTeacherReplace)
        {
            teacherReplacement.dateOfUpdate = DateTime.Now;
            teacherReplacement.teacherReplace = newTeacherReplace;
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(int id)
        {
            TeacherReplacement teacherReplacement = await findById(id);
            if (teacherReplacement == null) return false;
            _dbc.TeacherReplacements.Remove(teacherReplacement);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(TeacherReplacement teacherReplacement)
        {
            _dbc.TeacherReplacements.Remove(teacherReplacement);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(DateTime dateOfDay, DanceGroup danceGroup, DanceGroupDayOfWeek danceGroupDayOfWeek)
        {
            TeacherReplacement teacherReplacement = await find(dateOfDay, danceGroup, danceGroupDayOfWeek);
            _dbc.TeacherReplacements.Remove(teacherReplacement);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
