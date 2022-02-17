using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO;
using LilFamStudioNet5.DTO.Admin;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.Models.Admin;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers.api
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    public class ApiAdminController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiAdminController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }


        [Route("add")]
        public async Task<IActionResult> add([FromForm] AdminNewDTO adminNewDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                return Ok(await adminFacade.add(adminNewDTO));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }

        [Route("delete")]
        public async Task<IActionResult> delete([FromForm] AdminIdDTO adminIdDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                return Ok(await adminFacade.delete(adminIdDTO));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }


        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Search([FromForm] AdminSearchDTO adminSearchDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                return Ok(await adminFacade.search(adminSearchDTO));
            }
            return Ok();
        }




        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> get([FromForm] AdminIdDTO adminIdDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                AdminEditViewModel adminEditViewModel = await adminFacade.get(adminIdDTO.id_of_admin);

                return Ok(
                    adminEditViewModel != null
                    ? new JsonAnswerStatus("success", null, adminEditViewModel)
                    : new JsonAnswerStatus("error", "not_found")
                );
            }
            return Ok();
        }

        [Route("edit")]
        public async Task<IActionResult> editUpdate([FromForm] AdminEditDTO adminEditDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                return Ok(await adminFacade.editUpdate(adminEditDTO));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }



        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login([FromForm] AdminLoginDTO adminLoginDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                Admin admin = await adminFacade.login(adminLoginDTO);
                if (admin == null) return Ok(new JsonAnswerStatus("error", "wrong"));

                await loginCookie(admin);
                return Ok(new JsonAnswerStatus("success", null));
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> update([FromForm] AdminProfileDTO adminProfileDTO)
        {
            if (ModelState.IsValid)
            {
                AdminFacade adminFacade = new AdminFacade(_dbc);
                AdminSaveProfileResult adminSaveProfileResult = await adminFacade.update(HttpContext, adminProfileDTO);
                if (adminSaveProfileResult == null) return Ok(new JsonAnswerStatus("error", null)) ;
                if (adminSaveProfileResult.isNeedRelogin) await loginCookie(adminSaveProfileResult.admin);

                return Ok(new JsonAnswerStatus(adminSaveProfileResult.status, adminSaveProfileResult.errors));
            }
            return Ok(new JsonAnswerStatus("success", null));
        }

        private async Task loginCookie(Admin admin)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, admin.id.ToString()),
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AdminCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("AdminCookie", claimsPrincipal);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminCookie");
            return Ok(new JsonAnswerStatus("success", null));
        }

    }
}
