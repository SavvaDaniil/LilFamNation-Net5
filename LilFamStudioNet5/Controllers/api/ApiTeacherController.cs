using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Teacher;
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
    [Route("api/teacher")]
    [ApiController]
    public class ApiTeacherController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiTeacherController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] TeacherIdDTO teacherIdDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherFacade teacherFacade = new TeacherFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await teacherFacade.getEdit(teacherIdDTO.id_of_teacher)));
            }
            return Ok();
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] TeacherNewDTO teacherNewDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherFacade teacherFacade = new TeacherFacade(_dbc);
                return Ok(
                    await teacherFacade.add(teacherNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] TeacherByColumnDTO teacherByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherFacade teacherFacade = new TeacherFacade(_dbc);
                return Ok( await teacherFacade.update(teacherByColumnDTO) );
            }
            return Ok();
        }


        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] TeacherIdDTO teacherIdDTO)
        {
            if (ModelState.IsValid)
            {
                TeacherFacade teacherFacade = new TeacherFacade(_dbc);
                return Ok(
                    await teacherFacade.delete(teacherIdDTO.id_of_teacher)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

    }
}
