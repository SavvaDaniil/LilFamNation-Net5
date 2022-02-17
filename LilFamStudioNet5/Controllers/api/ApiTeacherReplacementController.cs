using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.TeacherReplacement;
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
    [Route("api/teacher_replacement")]
    [ApiController]
    public class ApiTeacherReplacementController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiTeacherReplacementController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] TeacherReplacementEditDTO teacherReplacementEditDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherReplacementFacade teacherReplacementFacade = new TeacherReplacementFacade(_dbc);
                return Ok(
                    await teacherReplacementFacade.getEdit(teacherReplacementEditDTO)
                );
            }
            return Ok();
        }


        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] TeacherReplacementUpdateDTO teacherReplacementUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherReplacementFacade teacherReplacementFacade = new TeacherReplacementFacade(_dbc);
                return Ok(
                    await teacherReplacementFacade.update(teacherReplacementUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
