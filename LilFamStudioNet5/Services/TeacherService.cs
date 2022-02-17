using LilFamStudioNet5.Data;
using LilFamStudioNet5.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Services
{
    public class TeacherService
    {
        public ApplicationDbContext _dbc;
        public TeacherService(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        public async Task<Teacher> findById(int id)
        {
            return await _dbc.Teachers.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<List<Teacher>> listAll()
        {
            return await _dbc.Teachers.OrderByDescending(p => p.id).ToListAsync();
        }

        public async Task<List<Teacher>> listAllOrderByName()
        {
            return await _dbc.Teachers.OrderBy(p => p.name).ToListAsync();
        }

        public async Task<Teacher> add(string name)
        {
            Teacher teacher = new Teacher();
            teacher.name = name;

            await _dbc.Teachers.AddAsync(teacher);
            await _dbc.SaveChangesAsync();

            return teacher;
        }

        public async Task<bool> updateByColumn(Teacher teacher, string name, string value)
        {
            switch (name)
            {
                case "name":
                    teacher.name = value;
                    break;
                case "stavka":
                    teacher.stavka = int.Parse(value);
                    break;
                case "min_students":
                    teacher.minStudents = int.Parse(value);
                    break;
                case "raz":
                    teacher.raz = int.Parse(value);
                    break;
                case "usual":
                    teacher.usual = int.Parse(value);
                    break;
                case "unlim":
                    teacher.unlim = int.Parse(value);
                    break;
                case "stavka_plus":
                    teacher.stavkaPlus = int.Parse(value);
                    break;
                case "plus_after_students":
                    teacher.plusAfterStudents = int.Parse(value);
                    break;
                case "plus_after_summa":
                    teacher.plusAfterSumma = int.Parse(value);
                    break;
                case "procent":
                    int procent = int.Parse(value);
                    teacher.procent = (procent > 100 || procent < 0 ? 0 : procent);
                    break;
                default:
                    break;
            }
            await _dbc.SaveChangesAsync();

            return true;
        }

        public async Task<bool> delete(int id)
        {
            Teacher teacher = await findById(id);
            if (teacher == null) return false;
            _dbc.Teachers.Remove(teacher);
            await _dbc.SaveChangesAsync();
            return true;
        }

        public async Task<bool> delete(Teacher teacher)
        {
            _dbc.Teachers.Remove(teacher);
            await _dbc.SaveChangesAsync();
            return true;
        }
    }
}
