using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.TeacherSalary;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class TeacherSalaryFacade
    {
        public ApplicationDbContext _dbc;
        public TeacherSalaryFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }




        ...
    }
}
