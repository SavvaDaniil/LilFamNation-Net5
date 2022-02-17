using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.ConnectionAbonementToDiscount;
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
    [Route("api/connection_abonement_to_discount")]
    [ApiController]
    public class ApiConnectionAbonementToDiscountController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiConnectionAbonementToDiscountController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] ConnectionAbonementToDiscountUpdateDTO connectionAbonementToDiscountUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                ConnectionAbonementToDiscountFacade connectionAbonementToDiscountFacade = new ConnectionAbonementToDiscountFacade(_dbc);
                return Ok(
                    await connectionAbonementToDiscountFacade.update(connectionAbonementToDiscountUpdateDTO)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
