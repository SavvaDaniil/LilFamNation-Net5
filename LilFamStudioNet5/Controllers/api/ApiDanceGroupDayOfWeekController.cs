using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.DanceGroupDayOfWeek;
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
    [Route("api/dance_group_day_of_week")]
    [ApiController]
    public class ApiDanceGroupDayOfWeekController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiDanceGroupDayOfWeekController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] DanceGroupDayOfWeekNewDTO danceGroupDayOfWeekNewDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
                return Ok(
                    await danceGroupDayOfWeekFacade.add(danceGroupDayOfWeekNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] DanceGroupDayOfWeekEditByColumnDTO danceGroupDayOfWeekEditByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
                return Ok(await danceGroupDayOfWeekFacade.update(danceGroupDayOfWeekEditByColumnDTO));
            }
            return Ok();
        }


        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] DanceGroupDayOfWeekIdDTO danceGroupDayOfWeekIdDTO)
        {
            if (ModelState.IsValid)
            {
                DanceGroupDayOfWeekFacade danceGroupDayOfWeekFacade = new DanceGroupDayOfWeekFacade(_dbc);
                return Ok(
                    await danceGroupDayOfWeekFacade.delete(danceGroupDayOfWeekIdDTO.id_of_dance_group_day_of_week)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
