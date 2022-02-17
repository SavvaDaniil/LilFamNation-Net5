using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.AbonementDynamicDateMustBeUsedTo;
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
    [Route("api/abonement_dynamic_date_must_be_used_to")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class ApiAbonementDynamicDateMustBeUsedToController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiAbonementDynamicDateMustBeUsedToController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("add")]
        public async Task<IActionResult> add([FromForm] AbonementDynamicDateMustBeUsedToNewDTO abonementDynamicDateMustBeUsedToNewDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementDynamicDateMustBeUsedToFacade abonementDynamicDateMustBeUsedToFacade = new AbonementDynamicDateMustBeUsedToFacade(_dbc);
                return Ok(
                    await abonementDynamicDateMustBeUsedToFacade.add(abonementDynamicDateMustBeUsedToNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        public async Task<IActionResult> update([FromForm] AbonementDynamicDateMustBeUsedToEditByColumnDTO abonementDynamicDateMustBeUsedToEditByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementDynamicDateMustBeUsedToFacade abonementDynamicDateMustBeUsedToFacade = new AbonementDynamicDateMustBeUsedToFacade(_dbc);
                return Ok(await abonementDynamicDateMustBeUsedToFacade.update(abonementDynamicDateMustBeUsedToEditByColumnDTO));
            }
            return Ok();
        }


        [Route("delete")]
        public async Task<IActionResult> delete([FromForm] AbonementDynamicDateMustBeUsedToIdDTO abonementDynamicDateMustBeUsedToIdDTO)
        {
            if (ModelState.IsValid)
            {
                AbonementDynamicDateMustBeUsedToFacade abonementDynamicDateMustBeUsedToFacade = new AbonementDynamicDateMustBeUsedToFacade(_dbc);
                return Ok(
                    await abonementDynamicDateMustBeUsedToFacade.delete(abonementDynamicDateMustBeUsedToIdDTO.id_of_abonement_dynamic_date_be_must_used_to)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
