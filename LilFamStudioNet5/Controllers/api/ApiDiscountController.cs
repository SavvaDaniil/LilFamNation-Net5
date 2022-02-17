using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Discount;
using LilFamStudioNet5.DTO.User;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers.api
{
    [Route("api/discount")]
    [ApiController]
    public class ApiDiscountController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiDiscountController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] DiscountNewDTO discountNewDTO)
        {
            if (ModelState.IsValid)
            {
                DiscountFacade discountFacade = new DiscountFacade(_dbc);
                return Ok(
                    await discountFacade.add(discountNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] DiscountEditByColumnDTO discountEditByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                DiscountFacade discountFacade = new DiscountFacade(_dbc);
                return Ok(await discountFacade.update(discountEditByColumnDTO));
            }
            return Ok();
        }


        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] DiscountIdDTO discountIdDTO)
        {
            if (ModelState.IsValid)
            {
                DiscountFacade discountFacade = new DiscountFacade(_dbc);
                return Ok(
                    await discountFacade.delete(discountIdDTO.id_of_discount)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }



        [Route("list_by_user")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listOfUser([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                DiscountFacade discountFacade = new DiscountFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await discountFacade.listAllWithConnectectionToUserLite(userIdDTO.id_of_user)));
            }
            return Ok();
        }


    }
}
