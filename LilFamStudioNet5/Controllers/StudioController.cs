using LilFamStudioNet5.Data;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class StudioController : Controller
    {
        private ApplicationDbContext _dbc;
        public StudioController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<IActionResult> Index()
        {
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "");
            return View(await adminFacade.getCurrentProfile(HttpContext));
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            ViewData["idMenuActive"] = 0;
            return View();
        }

        public async Task<IActionResult> Lessons()
        {
            DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelLessons");
            return View(new JsonAnswerStatus("success", null, await danceGroupFacade.listAllLite()));
        }

        public async Task<IActionResult> Teachers()
        {
            TeacherFacade teacherFacade = new TeacherFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelTeachers");
            return View(
                new JsonAnswerStatus("success", null, await teacherFacade.listAllLite())
            );
        }

        public async Task<IActionResult> Branches()
        {
            BranchFacade branchFacade = new BranchFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelBranches");
            return View(
                new JsonAnswerStatus("success", null, await branchFacade.listAllLite())
            );
        }

        public async Task<IActionResult> Abonements()
        {
            AbonementFacade abonementFacade = new AbonementFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelAbonements");
            return View(
                new JsonAnswerStatus("success", null, await abonementFacade.listAllLite())
            );
        }

        public async Task<IActionResult> DanceGroups()
        {
            DanceGroupFacade danceGroupFacade = new DanceGroupFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelDanceGroups");
            return View(
                new JsonAnswerStatus("success", null, await danceGroupFacade.listAllLite())
            );
        }

        public async Task<IActionResult> Discounts()
        {
            DiscountFacade discountFacade = new DiscountFacade(_dbc);
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelDiscounts");
            return View(
                new JsonAnswerStatus("success", null, await discountFacade.listAllLite())
            );
        }

        public async Task<IActionResult> Admins()
        {
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelAdmins");
            return View();
        }

        public async Task<IActionResult> Users()
        {
            ViewData["idMenuActive"] = 0;
            AdminFacade adminFacade = new AdminFacade(_dbc);
            ViewData["AdminAuthorizeViewModel"] = await adminFacade.getAuthorizeViewModel(HttpContext, "panelUsers");
            return View();
        }
    }
}
