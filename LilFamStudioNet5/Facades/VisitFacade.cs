using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Visit;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Models;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
using LilFamStudioNet5.ViewModels.PurchaseAbonement;
using LilFamStudioNet5.ViewModels.TeacherSalary;
using LilFamStudioNet5.ViewModels.Visit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class VisitFacade
    {
        public ApplicationDbContext _dbc;
        public VisitFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }




        ...
    }
}
