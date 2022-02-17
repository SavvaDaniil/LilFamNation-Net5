using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Abonement;
using LilFamStudioNet5.DTO.User;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
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
    [Route("api/abonement")]
    [ApiController]
    public class ApiAbonementController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiAbonementController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("list_private_connections_to_user")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listPrivateConnectionsToUser([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await abonementFacade.listAllPrivateConnectionsToUser(userIdDTO.id_of_user)));
            }
            return Ok();
        }



        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] AbonementIdDTO abonementIdDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await abonementFacade.getEdit(abonementIdDTO.id_of_abonement)));
            }
            return Ok();
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] AbonementNewDTO abonementNewDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(
                    await abonementFacade.add(abonementNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] AbonementEditByColumnDTO abonementEditByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(await abonementFacade.update(abonementEditByColumnDTO));
            }
            return Ok();
        }


        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] AbonementIdDTO abonementIdDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(
                    await abonementFacade.delete(abonementIdDTO.id_of_abonement)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("list_all_for_admin_to_user")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listAllForAdminToUser([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await abonementFacade.listsAllBySpecialStatusForUserByAdmin(userIdDTO.id_of_user)));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("app/list_all_for_user")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> listAllForUser()
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await abonementFacade.appListsAllBySpecialStatusForUser(HttpContext)));
            }
            return Ok();
        }



        [Route("get_for_buy")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getForBuy([FromForm] AbonementIdAndUserIdDTO abonementIdAndUserIdDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementFacade abonementFacade = new AbonementFacade(_dbc);
                AbonementForBuyingViewModel abonementForBuyingViewModel = await abonementFacade.getForBuy(abonementIdAndUserIdDTO.id_of_user, abonementIdAndUserIdDTO.id_of_abonement);
                if (abonementForBuyingViewModel == null) return Ok(new JsonAnswerStatus("error", "unknown"));
                return Ok(new JsonAnswerStatus("success", null, abonementForBuyingViewModel));
            }
            return Ok();
        }


    }
}
