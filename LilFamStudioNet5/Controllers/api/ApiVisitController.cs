using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.PurchaseAbonement;
using LilFamStudioNet5.DTO.User;
using LilFamStudioNet5.DTO.Visit;
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
    [Route("api/visit")]
    [ApiController]
    public class ApiVisitController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiVisitController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("prepare")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> prepare([FromForm] VisitPrepareDTO visitPrepareDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(
                    new JsonAnswerStatus(
                        "success", 
                        null, 
                        await visitFacade.prepareByAdmin(
                            visitPrepareDTO.id_of_user, 
                            visitPrepareDTO.id_of_dance_group, 
                            visitPrepareDTO.id_of_dance_group_day_of_week,
                            visitPrepareDTO.date_of_day
                        )
                    )
                );
            }
            return Ok();
        }

        [Route("app/prepare")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appPrepare([FromForm] VisitPrepareByUserDTO visitPrepareByUserDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(
                    new JsonAnswerStatus(
                        "success", 
                        null, 
                        await visitFacade.prepareByUser(
                            HttpContext,
                            visitPrepareByUserDTO.id_of_dance_group,
                            visitPrepareByUserDTO.id_of_dance_group_day_of_week,
                            visitPrepareByUserDTO.date_of_lesson
                        )
                    )
                );
            }
            return Ok();
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> addByAdmin([FromForm] VisitNewDTO visitNewDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.addByAdmin(visitNewDTO));
            }
            return Ok();
        }

        [Route("app/add")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> addByUser([FromForm] VisitNewByUserDTO visitNewByUserDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.addByUser(HttpContext, visitNewByUserDTO));
            }
            return Ok();
        }



        [Route("app/admin/get_with_purchase_abonement")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appUserAdminGetFullInfo([FromForm] VisitIdDTO visitIdDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.getFullInfoForAdmin(HttpContext, visitIdDTO.id_of_visit));
            }
            return Ok();
        }



        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> deleteByAdmin([FromForm] VisitIdDTO visitIdDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.deleteByAdmin(visitIdDTO.id_of_visit));
            }
            return Ok();
        }


        [Route("app/delete")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> deleteByUser([FromForm] VisitIdDTO visitIdDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok( await visitFacade.deleteByUser(HttpContext, visitIdDTO.id_of_visit));
            }
            return Ok();
        }

        [Route("app/admin/delete")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> deleteByUserAdmin([FromForm] VisitIdDTO visitIdDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.deleteByUserAdmin(HttpContext, visitIdDTO.id_of_visit));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }


        [Route("list_by_user")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listAllByUser([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await visitFacade.listAllByUser(userIdDTO.id_of_user)));
            }
            return Ok();
        }

        [Route("app/list_by_user")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appListAllByUser()
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await visitFacade.listAllByUser(HttpContext)));
            }
            return Ok();
        }

        [Route("app/list_by_user_and_purchase_abonement")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appListAllByUser([FromForm] PurchaseAbonementIdDTO purchaseAbonementIdDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await visitFacade.listAllByUser(HttpContext, purchaseAbonementIdDTO.id_of_purchase_abonement)));
            }
            return Ok();
        }



        [Route("list_all_visit_lessons")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listAllAsVisitLessons([FromForm] VisitLessonFilterDTO visitLessonFilterDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await visitFacade.listAllAsVisitLessons(visitLessonFilterDTO)));
            }
            return Ok();
        }


        [Route("list_all_by_date")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listByDateOfBuyForAnalitycs([FromForm] VisitAnalyticsDTO visitAnalyticsDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.listByDateOfBuyForAnalytics(visitAnalyticsDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }


        [Route("app/admin/list_all_for_lesson")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appListByDateOfBuyForAnalitycs([FromForm] VisitAnalyticsDTO visitAnalyticsDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.listByDateOfBuyAndDanceGroupForUserAdmin(HttpContext, visitAnalyticsDTO));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }


        [Route("app/admin/reservation/update")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appReservationUpdate([FromForm] VisitReservationDTO visitReservationDTO)
        {
            if (ModelState.IsValid)
            {
                VisitFacade visitFacade = new VisitFacade(_dbc);
                return Ok(await visitFacade.reservationUpdateByAdmin(HttpContext, visitReservationDTO.id_of_visit, visitReservationDTO.action));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }


    }
}
