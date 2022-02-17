using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionUserToDanceGroup;
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
    [Route("api/connection_user_to_dance_group")]
    [ApiController]
    public class ApiConnectionUserToDanceGroupController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiConnectionUserToDanceGroupController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] ConnectionUserToDanceGroupUpdateDTO connectionUserToDanceGroupUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                ConnectionUserToDanceGroupFacade connectionUserToDanceGroupFacade = new ConnectionUserToDanceGroupFacade(_dbc);
                return Ok(
                    await connectionUserToDanceGroupFacade.update(connectionUserToDanceGroupUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
