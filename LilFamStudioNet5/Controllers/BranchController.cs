using LilFamStudioNet5.Data;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Branch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class BranchController : Controller
    {
        private ApplicationDbContext _dbc;
        public BranchController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [HttpGet]
        [Route("/studio/branch/edit/{id_of_branch}")]
        public async Task<IActionResult> Edit(int id_of_branch)
        {
            BranchFacade branchFacade = new BranchFacade(_dbc);
            BranchEditViewModel branchEditViewModel = await branchFacade.getEdit(id_of_branch);
            if (branchEditViewModel == null) return Redirect("/studio/branches");
            return View(new JsonAnswerStatus("success", null, branchEditViewModel));
        }
    }
}
