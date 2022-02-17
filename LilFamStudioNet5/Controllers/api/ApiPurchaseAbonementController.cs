using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.DanceGroup;
using LilFamStudioNet5.DTO.PurchaseAbonement;
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
    [Route("api/purchase_abonement")]
    [ApiController]
    public class ApiPurchaseAbonementController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiPurchaseAbonementController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] PurchaseAbonementNewDTO purchaseAbonementNewDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(await purchaseAbonementFacade.addByAdmin(purchaseAbonementNewDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("app/admin/get")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] PurchaseAbonementIdDTO purchaseAbonementIdDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(await purchaseAbonementFacade.getForUserAdmin(HttpContext, purchaseAbonementIdDTO.id_of_purchase_abonement));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] PurchaseAbonementIdDTO purchaseAbonementIdDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(
                    await purchaseAbonementFacade.delete(purchaseAbonementIdDTO.id_of_purchase_abonement)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("list_all_active_for_user")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listAllActiveForUser([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await purchaseAbonementFacade.listAllActiveByIdOfUser(userIdDTO.id_of_user)));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("app/list_all")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appListAll([FromForm] DanceGroupIdDTO danceGroupIdDTO)
        {
            PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
            return Ok(new JsonAnswerStatus("success", null, await purchaseAbonementFacade.listAllForUser(HttpContext, danceGroupIdDTO.id_of_dance_group)));
        }


        [Route("list_all_edit_for_user")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listAllEditForUser([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await purchaseAbonementFacade.listAllEditByIdOfUser(userIdDTO.id_of_user)));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("list_all_by_date")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listByDateOfBuyForAnalitycs([FromForm] PurchaseAbonementAnalyticsDTO purchaseAbonementAnalyticsDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(await purchaseAbonementFacade.listByDateOfBuyForAnalitycs(purchaseAbonementAnalyticsDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }



        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] PurchaseAbonementIdDTO purchaseAbonementIdDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(await purchaseAbonementFacade.getEdit(purchaseAbonementIdDTO.id_of_purchase_abonement));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] PurchaseAbonementDTO purchaseAbonementDTO)
        {
            if (ModelState.IsValid)
            {
                PurchaseAbonementFacade purchaseAbonementFacade = new PurchaseAbonementFacade(_dbc);
                return Ok(await purchaseAbonementFacade.update(purchaseAbonementDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }


    }
}
