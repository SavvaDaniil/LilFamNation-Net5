using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionAbonementPrivateToUser;
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
    [Route("api/connection_abonement_private_to_user")]
    [ApiController]
    public class ApiConnectionAbonementPrivateToUserController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiConnectionAbonementPrivateToUserController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] ConnectionAbonementPrivateToUserUpdateDTO connectionAbonementPrivateToUserUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                ConnectionAbonementPrivateToUserFacade connectionAbonementPrivateToUserFacade = new ConnectionAbonementPrivateToUserFacade(_dbc);
                return Ok(
                    await connectionAbonementPrivateToUserFacade.update(connectionAbonementPrivateToUserUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
