using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionDiscountToUser;
using LilFamStudioNet5.DTO.User;
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
    [Route("api/connection_discount_to_user")]
    [ApiController]
    public class ApiConnectionDiscountToUserController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiConnectionDiscountToUserController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] ConnectionDiscountToUserUpdateDTO connectionDiscountToUserUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                ConnectionDiscountToUserFacade connectionDiscountToUserFacade = new ConnectionDiscountToUserFacade(_dbc);
                return Ok(
                    await connectionDiscountToUserFacade.update(connectionDiscountToUserUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

    }
}
