using LilFamStudioNet5.Data;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Abonement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class AbonementController : Controller
    {
        private ApplicationDbContext _dbc;
        public AbonementController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        [HttpGet]
        [Route("/studio/abonement/edit/{id_of_abonement}")]
        public async Task<IActionResult> Edit(int id_of_abonement)
        {
            AbonementFacade abonementFacade = new AbonementFacade(_dbc);
            AbonementEditViewModel abonementEditViewModel = await abonementFacade.getEdit(id_of_abonement);
            if (abonementEditViewModel == null) return Redirect("/studio/abonements");
            return View(new JsonAnswerStatus("success", null, abonementEditViewModel));
        }
    }
}
