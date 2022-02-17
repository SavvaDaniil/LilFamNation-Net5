using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionDanceGroupToUserAdmin;
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
    [Route("api/connection_dance_group_to_user_admin")]
    [ApiController]
    public class ApiConnectionDanceGroupToUserAdminController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiConnectionDanceGroupToUserAdminController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] ConnectionDanceGroupToUserAdminUpdateDTO connectionDanceGroupToUserAdminUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                ConnectionDanceGroupToUserAdminFacade connectionDanceGroupToUserAdminFacade = new ConnectionDanceGroupToUserAdminFacade(_dbc);
                return Ok(
                    await connectionDanceGroupToUserAdminFacade.update(connectionDanceGroupToUserAdminUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
