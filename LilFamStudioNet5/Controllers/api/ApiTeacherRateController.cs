using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.TeacherRate;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers.api
{
    [Route("api/teacher_rate")]
    [ApiController]
    public class ApiTeacherRateController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiTeacherRateController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] TeacherRateNewDTO teacherRateNewDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherRateFacade teacherRateFacade = new TeacherRateFacade(_dbc);
                return Ok(await teacherRateFacade.add(teacherRateNewDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] TeacherRateDTO teacherRateDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherRateFacade teacherRateFacade = new TeacherRateFacade(_dbc);
                return Ok(await teacherRateFacade.update(teacherRateDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] TeacherRateIdDTO teacherRateIdDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherRateFacade teacherRateFacade = new TeacherRateFacade(_dbc);
                return Ok(await teacherRateFacade.deleteByTeacherRateIdDTO(teacherRateIdDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }
    }
}
