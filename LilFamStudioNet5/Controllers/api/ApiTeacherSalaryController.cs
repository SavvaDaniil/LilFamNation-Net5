using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.TeacherSalary;
using LilFamStudioNet5.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers.api
{
    [Route("api/teacher_salary")]
    [ApiController]
    public class ApiTeacherSalaryController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiTeacherSalaryController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update_price_fact")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> updatePriceFact([FromForm] TeacherSalaryUpdatePriceDTO teacherSalaryUpdatePriceDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherSalaryFacade teacherSalaryFacade = new TeacherSalaryFacade(_dbc);
                return Ok(await teacherSalaryFacade.updatePriceFact(teacherSalaryUpdatePriceDTO));
            }
            return Ok();
        }
    }
}
