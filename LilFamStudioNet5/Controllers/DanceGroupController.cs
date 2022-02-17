using LilFamStudioNet5.Data;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.DanceGroup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers
{
    public class DanceGroupController : Controller
    {
        private ApplicationDbContext _dbc;
        public DanceGroupController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [HttpGet]
        [Route("/studio/dance_group/edit/{id_of_dance_group}")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> Edit(int id_of_dance_group)
        {
            DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
            DanceGroupEditViewModel danceGroupEditViewModel = await danceGroupFacade.getEdit(id_of_dance_group);
            if (danceGroupEditViewModel == null) return Redirect("/studio/dance_groups");
            return View(new JsonAnswerStatus("success", null, danceGroupEditViewModel));
        }
    }
}
