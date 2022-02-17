using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionAbonementToDanceGroup;
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
    [Route("api/connection_abonement_to_dance_group")]
    [ApiController]
    public class ApiConnectionAbonementToDanceGroupController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiConnectionAbonementToDanceGroupController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] ConnectionAbonementToDanceGroupUpdateDTO connectionAbonementToDanceGroupUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                ConnectionAbonementToDanceGroupFacade connectionAbonementToDanceGroupFacade = new ConnectionAbonementToDanceGroupFacade(_dbc);
                return Ok(
                    await connectionAbonementToDanceGroupFacade.update(connectionAbonementToDanceGroupUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

    }
}
