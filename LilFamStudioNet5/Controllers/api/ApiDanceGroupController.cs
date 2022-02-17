using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.DanceGroup;
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
    [Route("api/dance_group")]
    [ApiController]
    public class ApiDanceGroupController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiDanceGroupController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("list_connections_to_user_admin")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listConnectionsToUserAdmin([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await danceGroupFacade.listAllWithConnectectionToUserAdmin(userIdDTO.id_of_user)));
            }
            return Ok();
        }

        [Route("list_by_day_of_week")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> listAllByDanceGroupDayOfWeek()
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await danceGroupFacade.listAllByDanceGroupDayOfWeek()));
            }
            return Ok();
        }



        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] DanceGroupIdDTO danceGroupIdDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await danceGroupFacade.getEdit(danceGroupIdDTO.id_of_dance_group)));
            }
            return Ok();
        }

        [Route("get_schedule")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getSchedule([FromForm] DanceGroupDateDTO danceGroupDateDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(
                    new JsonAnswerStatus(
                    "success", 
                    null, 
                    await danceGroupFacade.getScheduleByDate(danceGroupDateDTO.date_of_day))
                );
            }
            return Ok();
        }

        [Route("get_schedule_by_dates")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        [AllowAnonymous]
        public async Task<IActionResult> getScheduleByDates([FromForm] DanceGroupScheduleDatesDTO danceGroupScheduleDatesDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(
                    new JsonAnswerStatus(
                    "success",
                    null,
                    await danceGroupFacade.getScheduleFromDateToDate(HttpContext, danceGroupScheduleDatesDTO.filterDateFromStr, danceGroupScheduleDatesDTO.filterDateToStr, true))
                );
            }
            return Ok();
        }

        [Route("app/admin/schedule/get")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        [AllowAnonymous]
        public async Task<IActionResult> getScheduleForUserAdmin()
        {
            DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
            return Ok(
                new JsonAnswerStatus(
                "success",
                null,
                await danceGroupFacade.getScheduleForAppUserAdmin(HttpContext))
            );
        }

        [Route("app/get")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        [AllowAnonymous]
        public async Task<IActionResult> appGet([FromForm] DanceGroupGetDTO danceGroupGetDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(
                    await danceGroupFacade.getForApp(
                        HttpContext, 
                        danceGroupGetDTO.id_of_dance_group, 
                        danceGroupGetDTO.id_of_dance_group_day_of_week, 
                        danceGroupGetDTO.date_of_lesson_str,
                        danceGroupGetDTO.id_of_visit
                    )
                );
            }
            return Ok();
        }



        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] DanceGroupNewDTO danceGroupNewDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(
                    await danceGroupFacade.add(danceGroupNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] DanceGroupEditByColumnDTO danceGroupEditByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(await danceGroupFacade.update(danceGroupEditByColumnDTO));
            }
            return Ok();
        }


        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] DanceGroupIdDTO danceGroupIdDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
                return Ok(
                    await danceGroupFacade.delete(danceGroupIdDTO.id_of_dance_group)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
