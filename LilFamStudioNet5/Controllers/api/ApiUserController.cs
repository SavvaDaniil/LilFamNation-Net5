using LilFamStudioNet5.Components;
using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.User;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Facades;
using LilFamStudioNet5.Models.User;
using LilFamStudioNet5.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Controllers.api
{
    [Route("api/user")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private ApplicationDbContext _dbc;
        public ApiUserController(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }




        [HttpPost]
        [Route("add")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> add([FromForm] UserNewDTO userNewDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(
                    await userFacade.add(userNewDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok();
        }


        [HttpPost]
        [Route("search")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> search([FromForm] UserSearchDTO userSearchDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(await userFacade.search(userSearchDTO));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }


        [HttpPost]
        [Route("search_lite")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> searchLite([FromForm] UserSearchLiteDTO userSearchLiteDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(await userFacade.searchLite(userSearchLiteDTO));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }

        [HttpPost]
        [Route("get_edit")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> getEdit([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(await userFacade.getEdit(userIdDTO.id_of_user));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }

        [HttpPost]
        [Route("app/profile")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appProfile()
        {
            UserFacade userFacade = new UserFacade(_dbc);
            return Ok(await userFacade.getProfile(HttpContext));
        }

        [HttpPost]
        [Route("update_status_of_admin")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> updateStatusOfAdmin([FromForm] UserUpdateStatusOfAdminDTO updateStatusOfAdminDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(await userFacade.updateUserStatusOfAdmin(updateStatusOfAdminDTO.id_of_user, updateStatusOfAdminDTO.status));
            }
            return Ok(new JsonAnswerStatus("error", null));
        }

        [HttpPost]
        [Route("update_by_admin")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> updateByAdmin([FromForm] UserProfileByAdminDTO userProfileByAdminDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);

                return Ok(
                    await userFacade.updateByAdmin(userProfileByAdminDTO) != null
                    ? new JsonAnswerStatus("success", null)
                    : new JsonAnswerStatus("error", "unknown")
                );
            }
            return Ok(new JsonAnswerStatus("error", null));
        }

        [HttpPost]
        [Route("app/update_by_user")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> updateByUser([FromForm] UserProfileByUserDTO userProfileByUserDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);

                UserSaveProfileResult userSaveProfileResult = await userFacade.updateByUser(HttpContext, userProfileByUserDTO);
                JsonAnswerStatus jsonAnswerStatus = new JsonAnswerStatus(userSaveProfileResult.status, userSaveProfileResult.errors);
                if (userSaveProfileResult.isNeedRelogin && jsonAnswerStatus.status == "success" && userSaveProfileResult.user != null)
                {
                    jsonAnswerStatus.accessToken = generateJWT(userSaveProfileResult.user);
                }

                return Ok(jsonAnswerStatus);
            }
            return Ok(new JsonAnswerStatus("error", null));
        }

        [HttpPost]
        [Route("app/admin/status")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appCheckOfAdminStatus()
        {
            UserFacade userFacade = new UserFacade(_dbc);
            return Ok(await userFacade.checkStatusOfAdmin(HttpContext));
        }


        [HttpPost]
        [Route("app/qr/generate")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appQRGenerate()
        {
            UserFacade userFacade = new UserFacade(_dbc);
            return Ok(await userFacade.generateCheckQR(HttpContext));
        }


        [HttpPost]
        [Route("app/admin/qr/check")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appCheckQR([FromForm] UserCheckQRDTO userCheckQRDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(await userFacade.checkUserQRByAdmin(HttpContext, userCheckQRDTO.id_of_user, userCheckQRDTO.checkQR));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }






        [HttpPost]
        [AllowAnonymous]
        [Route("app/forget")]
        public async Task<IActionResult> forget([FromForm] UserForgetDTO userForgetDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(this._dbc);

                UserLoginResult userLoginResult = await userFacade.forget(userForgetDTO, true);
                JsonAnswerStatus jsonAnswerStatus = new JsonAnswerStatus(userLoginResult.status, userLoginResult.errors);
                if (userLoginResult.id_of_user != 0) jsonAnswerStatus.id_of_user = userLoginResult.id_of_user;
                if (userLoginResult.user != null)
                {
                    jsonAnswerStatus.accessToken = generateJWT(userLoginResult.user);
                }
                return Ok(jsonAnswerStatus);
            }
            return Ok();
        }


        [HttpPost]
        [Route("delete")]
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> delete([FromForm] UserIdDTO userIdDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                return Ok(await userFacade.delete(userIdDTO.id_of_user));
            }
            return Ok(new JsonAnswerStatus("error", "no_data"));
        }






        [HttpPost]
        [Route("app/registration")]
        public async Task<IActionResult> appRegistration([FromForm] UserRegistrationDTO userRegistrationDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                UserLoginResult userRegistrationResult = await userFacade.registration(userRegistrationDTO);
                JsonAnswerStatus jsonAnswerStatus = new JsonAnswerStatus(userRegistrationResult.status, userRegistrationResult.errors);
                if (userRegistrationResult.user != null)
                {
                    jsonAnswerStatus = new JsonAnswerStatus("success", null);
                    jsonAnswerStatus.accessToken = this.generateJWT(userRegistrationResult.user);
                }

                return Ok(jsonAnswerStatus);
            }

            return Ok(new JsonAnswerStatus("error", "no_data"));
        }




        [HttpPost]
        [Route("app/login")]
        public async Task<IActionResult> appLogin([FromForm] UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                UserFacade userFacade = new UserFacade(_dbc);
                UserLoginResult userLoginResult = await userFacade.login(userLoginDTO);

                JsonAnswerStatus jsonAnswerStatus = new JsonAnswerStatus(userLoginResult.status, userLoginResult.errors);

                if (userLoginResult.user != null)
                {
                    jsonAnswerStatus = new JsonAnswerStatus("success", null);
                    jsonAnswerStatus.accessToken = this.generateJWT(userLoginResult.user);
                }
                return Ok(jsonAnswerStatus);
            }
            else
            {
                return Ok(new JsonAnswerStatus("error", "no_data"));
            }

        }


        private string generateJWT(User user)
        {
            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                        new Claim(ClaimTypes.Name, user.username),
                        new Claim(ClaimTypes.Role, "User")
                    };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "UserJWT", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthUserJWTOptions.ISSUER,
                    audience: AuthUserJWTOptions.AUDIENCE,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthUserJWTOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthUserJWTOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        [Route("app/secret")]
        [Authorize(AuthenticationSchemes = "UserJWT")]
        public async Task<IActionResult> appSecret()
        {
            UserFacade userFacade = new UserFacade(this._dbc);
            User user = await userFacade.getCurrentOrNull(HttpContext);
            int id = (user != null ? user.id : 0);

            return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }


    }
}
