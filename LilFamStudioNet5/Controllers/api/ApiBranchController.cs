using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.Branch;
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
    [Route("api/branch")]
    [ApiController]
    public class ApiBranchController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiBranchController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] BranchIdDTO branchIdDTO)
        {
            if (ModelState.IsValid)
            {
                BranchFacade branchFacade = new BranchFacade(_dbc);
                return Ok(new JsonAnswerStatus("success", null, await branchFacade.getEdit(branchIdDTO.id_of_branch)));
            }
            return Ok();
        }

        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] BranchNewDTO branchNewDTO)
        {
            if (ModelState.IsValid)
            {
                BranchFacade branchFacade = new BranchFacade(_dbc);
                return Ok(
                    await branchFacade.add(branchNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }

        [Route("update")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> update([FromForm] BranchEditByColumnDTO branchEditByColumnDTO)
        {
            if (ModelState.IsValid)
            {
                BranchFacade branchFacade = new BranchFacade(_dbc);
                return Ok(await branchFacade.update(branchEditByColumnDTO));
            }
            return Ok();
        }


        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] BranchIdDTO branchIdDTO)
        {
            if (ModelState.IsValid)
            {
                BranchFacade branchFacade = new BranchFacade(_dbc);
                return Ok(
                    await branchFacade.delete(branchIdDTO.id_of_branch)
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }
    }
}
