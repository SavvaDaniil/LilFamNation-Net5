using LilFamStudioNet5.Components;
using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO.User;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Models.User;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace LilFamStudioNet5.Facades
{
    public class UserFacade
    {
        public ApplicationDbContext _dbc;
        public UserFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<User> add(UserNewDTO userNewDTO)
        {
            UserService userService = new UserService(_dbc);
            if (userNewDTO.fio == null) return null;
            User user = await userService.add(userNewDTO.fio, userNewDTO.phone, userNewDTO.comment);

            return user;
        }

        public async Task<UserLoginResult> registration(UserRegistrationDTO userRegistrationDTO)
        {
            if (userRegistrationDTO.username == null) return new UserLoginResult("error","no_data");
            UserService userService = new UserService(_dbc);
            if(await userService.isUsernameAlreadyExist(userRegistrationDTO.username))return new UserLoginResult("error", "username_already_exist");

            DateTime dateOfBirthday = DateTime.Now;
            if (userRegistrationDTO.dateOfBirthdayStr != null && userRegistrationDTO.dateOfBirthdayStr != "" && userRegistrationDTO.dateOfBirthdayStr != "null")
            {
                if(!DateTime.TryParse(userRegistrationDTO.dateOfBirthdayStr, out dateOfBirthday))
                {
                    return new UserLoginResult("error", "wrong_date_of_birthday");
                }
            } else
            {
                return new UserLoginResult("error", "wrong_date_of_birthday");
            }

            User user = await userService.add(
                userRegistrationDTO.username,
                userRegistrationDTO.password,
                userRegistrationDTO.fio,
                userRegistrationDTO.phone,
                userRegistrationDTO.sex,
                dateOfBirthday,
                userRegistrationDTO.parentFio,
                userRegistrationDTO.parentPhone
            );

            return new UserLoginResult("success", null, user);
        }


        public async Task<UserLoginResult> forget(UserForgetDTO userForgetDTO, bool isNeedJWTAuth = false)
        {
            UserService userService = new UserService(_dbc);

            if (userForgetDTO.step == 0)
            {
                if (userForgetDTO.username == null) return new UserLoginResult("error", "no_username");
                User user = await userService.findByUsername(userForgetDTO.username);
                if (user == null) return new UserLoginResult("error", "not_found");
                if (user.forgetDateOfLastTry == null) user.forgetDateOfLastTry = DateTime.Now;
                if (user.forgetDateOfLastTry.Value.AddMinutes(20) > DateTime.Now)
                {
                    if (user.forgetCount > 2) return new UserLoginResult("error", "max_limit_try");
                }
                string code = RandomComponent.RandomIntAsString(6);
                await userService.updateUserForgetCode(user, code);

                sendCodeToUser(user.username, code);

                return new UserLoginResult("success", null, user.id);
            }
            else if (userForgetDTO.step == 1)
            {
                if (userForgetDTO.code == null || userForgetDTO.forget_id == 0) return new UserLoginResult("error", "no_data");
                User user = await userService.findById(userForgetDTO.forget_id);
                if (user == null) return new UserLoginResult("error", "not_found");

                if (user.forgetCode != userForgetDTO.code)
                {
                    if (user.forgetCount > 2)
                    {
                        return new UserLoginResult("error", "wrong_max_limit");
                    }
                    else if (user.forgetCount > 1)
                    {
                        await userService.forgetUpdateCount(user, 3);
                        return new UserLoginResult("error", "wrong_2");
                    }
                    else if (user.forgetCount > 0)
                    {
                        await userService.forgetUpdateCount(user, 2);
                        return new UserLoginResult("error", "wrong_1");
                    }
                    else
                    {
                        await userService.forgetUpdateCount(user, 1);
                        return new UserLoginResult("error", "wrong");
                    }
                }
                string newPassword = RandomComponent.RandomString(6);
                if (!await userService.updatePassword(user, newPassword)) return new UserLoginResult("error", "when_update_password");

                //отправляем письмо с новым паролем
                sendNewPasswordToUser(user.username, newPassword);

                if (isNeedJWTAuth) return new UserLoginResult("success", null, user);


                return new UserLoginResult("success", null);
            }

            return new UserLoginResult("error", null);
        }


        public void sendCodeToUser(string username, string code)
        {
            if (username == null || code == null) return;
            MailSender mailSender = new MailSender();
            mailSender.sendMailToUser(
                username,
                "Код восстановление пароля",
                "Код:  <b>" + code + "</b>"
            );
        }

        public void sendNewPasswordToUser(string username, string password)
        {
            if (username == null || password == null) return;
            MailSender mailSender = new MailSender();
            mailSender.sendMailToUser(
                username,
                "Установлен новый пароль",
                "Новый пароль: <b>" + password + "</b>"
            );
        }




        public async Task<bool> delete(int id)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id);
            if (user == null) return false;

            return await userService.delete(user);
        }

        public async Task<JsonAnswerStatus> getEdit(int id)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id);
            if (user == null) return new JsonAnswerStatus("error", "not_found");

            return new JsonAnswerStatus("success", null,
                new UserProfileViewModel(
                    user.id,
                    user.fio,
                    user.phone,
                    user.sex,
                    (user.dateOfBirthday != null ? user.dateOfBirthday.Value.ToString("yyyy-MM-dd") : null),
                    user.username,
                    user.comment,
                    user.parentFio,
                    user.parentPhone,
                    user.statusOfAdmin
                )
            );
        }

        public async Task<JsonAnswerStatus> getProfile(HttpContext httpContext)
        {
            User user = await getCurrentOrNull(httpContext);
            if (user == null) return new JsonAnswerStatus("error", "not_found");

            return new JsonAnswerStatus("success", null,
                new UserProfileViewModel(
                    user.id,
                    user.fio,
                    user.phone,
                    user.sex,
                    (user.dateOfBirthday != null ? user.dateOfBirthday.Value.ToString("yyyy-MM-dd") : null),
                    user.username,
                    user.parentFio,
                    user.parentPhone
                )
            );
        }


        public async Task<UserLoginResult> login(UserLoginDTO userLoginDTO)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findByUsername(userLoginDTO.username);
            if (user == null) return new UserLoginResult("error", "not_found");

            if (BCrypt.Net.BCrypt.Verify(userLoginDTO.password, user.password)) return new UserLoginResult("success", null, user);
            return new UserLoginResult("error", "wrong"); ;
        }

        public async Task<UserProfileViewModel> getCurrentProfile(HttpContext httpContext)
        {
            User user = await this.getCurrentOrNull(httpContext);
            if (user == null) return null;
            return null;
        }


        public async Task<JsonAnswerStatus> search(UserSearchDTO userSearchDTO)
        {
            UserService userService = new UserService(_dbc);
            List<User> usersSearchResult = await userService.searchUsers(userSearchDTO);
            int searchCount = await userService.searchCount(userSearchDTO);

            List<UserSearchPreviewViewModel> userSearchPreviewViewModels = new List<UserSearchPreviewViewModel>();
            foreach (User user in usersSearchResult)
            {
                userSearchPreviewViewModels.Add(new UserSearchPreviewViewModel(
                    user.id,
                    user.fio,
                    user.phone,
                    user.dateOfAdd,
                    user.dateOfLastVisit
                    //(user.dateOfAdd != null ? user.dateOfAdd.Value.ToString("HH:mm:ss dd/MM/yyyy") : "-"),
                    //(user.dateOfLastVisit != null ? user.dateOfLastVisit.Value.ToString("HH:mm:ss dd/MM/yyyy") : "-")
                    )
                );
            }

            return new JsonAnswerStatus("success", null, userSearchPreviewViewModels, searchCount);
        }

        public async Task<JsonAnswerStatus> searchLite(UserSearchLiteDTO userSearchLiteDTO)
        {
            UserService userService = new UserService(_dbc);
            List<User> usersSearchResult = await userService.searchUsers(userSearchLiteDTO.queryString);

            List<UserSearchPreviewViewModel> userSearchPreviewViewModels = new List<UserSearchPreviewViewModel>();
            foreach (User user in usersSearchResult)
            {
                userSearchPreviewViewModels.Add(new UserSearchPreviewViewModel(
                    user.id,
                    user.fio,
                    user.phone,
                    user.dateOfAdd,
                    user.dateOfLastVisit
                    )
                );
            }

            return new JsonAnswerStatus("success", null, userSearchPreviewViewModels);
        }

        public async Task<UserSaveProfileResult> updateByAdmin(UserProfileByAdminDTO userProfileByAdminDTO)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(userProfileByAdminDTO.id_of_user);
            if (user == null) return null;

            if (userProfileByAdminDTO.username != user.username)
            {
                if (userService.isAlreadyExistByUsernameExceptId(user.id, userProfileByAdminDTO.username))
                {
                    return new UserSaveProfileResult("error", "username_already_exist");
                }
            }


            UserSaveProfileResult userSaveProfileResult = new UserSaveProfileResult(user);
            if (userProfileByAdminDTO.passwordNew != null || userProfileByAdminDTO.username != user.username) userSaveProfileResult.isNeedRelogin = true;

            if (await userService.update(
                    user,
                    userProfileByAdminDTO.fio,
                    userProfileByAdminDTO.phone,
                    userProfileByAdminDTO.sex,
                    userProfileByAdminDTO.username,
                    userProfileByAdminDTO.comment,
                    userProfileByAdminDTO.parentFio,
                    userProfileByAdminDTO.parentPhone,
                    userProfileByAdminDTO.passwordNew
                )
            )
            {
                userSaveProfileResult.status = "success";
            }
            else
            {
                userSaveProfileResult.status = "error";
            }

            return userSaveProfileResult;
        }


        public async Task<UserSaveProfileResult> updateByUser(HttpContext httpContext, UserProfileByUserDTO userProfileByUserDTO)
        {
            User user = await getCurrentOrNull(httpContext);
            if (user == null) return new UserSaveProfileResult("error", "not_found");

            UserService userService = new UserService(_dbc);
            if (userProfileByUserDTO.username != user.username)
            {
                if (userService.isAlreadyExistByUsernameExceptId(user.id, userProfileByUserDTO.username))
                {
                    return new UserSaveProfileResult("error", "username_already_exist");
                }
            }

            UserSaveProfileResult userSaveProfileResult = new UserSaveProfileResult();

            DateTime dateOfBirthday = DateTime.Now.Date;
            DateTime.TryParse(userProfileByUserDTO.dateOfBirthday, out dateOfBirthday);

            DateTime today = DateTime.Now.Date;
            int age = today.Year - dateOfBirthday.Year;
            if (dateOfBirthday > today.AddYears(-age)) age--;
            if(age < 18 && (userProfileByUserDTO.parentFio == null || userProfileByUserDTO.parentPhone == null))
            {
                return new UserSaveProfileResult("error", "need_parent_info");
            }

            string passwordNew = null;
            if (userProfileByUserDTO.passwordNew != null && userProfileByUserDTO.passwordNew != "")
            {
                if(userProfileByUserDTO.passwordCurrent == null) return new UserSaveProfileResult("error", "wrong");
                if (!BCrypt.Net.BCrypt.Verify(userProfileByUserDTO.passwordCurrent, user.password)) return new UserSaveProfileResult("error", "wrong");
                userSaveProfileResult.isNeedRelogin = true;
                passwordNew = userProfileByUserDTO.passwordNew;
            }
            if (userProfileByUserDTO.username != user.username)
            {
                userSaveProfileResult.isNeedRelogin = true;
            }

            User userUpdated = await userService.update(
                    user,
                    userProfileByUserDTO.fio,
                    userProfileByUserDTO.phone,
                    userProfileByUserDTO.sex,
                    dateOfBirthday,
                    userProfileByUserDTO.username,
                    userProfileByUserDTO.parentFio,
                    userProfileByUserDTO.parentPhone,
                    passwordNew
                );
            if (userUpdated != null)
            {
                userSaveProfileResult.status = "success";
            }
            else
            {
                userSaveProfileResult.status = "error";
            }

            userSaveProfileResult.user = userUpdated;

            return userSaveProfileResult;
        }

        public async Task<JsonAnswerStatus> updateUserStatusOfAdmin(int id_of_user, int status)
        {
            UserService userService = new UserService(_dbc);
            User user = await userService.findById(id_of_user);
            if (user == null) return new JsonAnswerStatus("error", "not_found");

            if (!await userService.updateUserStatusOfAdmin(user, status)) return new JsonAnswerStatus("error", "unknown");
            return new JsonAnswerStatus("success", null);
        }


        public async Task<JsonUserQRCode> generateCheckQR(HttpContext httpContext)
        {
            User user = await getCurrentOrNull(httpContext);
            if (user == null) return new JsonUserQRCode("error", "not_auth");

            string checkQRNew = RandomComponent.RandomString(32);
            UserService userService = new UserService(_dbc);
            if (! await userService.updateCheckQR(user, checkQRNew)) return new JsonUserQRCode("error", "when_trying_save_qr_code");

            string qrCodeAsBase64 = generateQRCodeAsBase64(user.id, checkQRNew);
            if(qrCodeAsBase64 == null) return new JsonUserQRCode("error", "when_trying_generate_qr_code");

            return new JsonUserQRCode("success", null, qrCodeAsBase64);
        }

        private string generateQRCodeAsBase64(int id_of_user, string checkQR)
        {
            UserQRCode userQRCode = new UserQRCode(id_of_user, checkQR);

            QRCodeWriter qrCodeWriter = new QRCodeWriter();
            BitMatrix bitMatrix = qrCodeWriter.encode(userQRCode.ToString(), BarcodeFormat.QR_CODE, 500, 500);

            try
            {
                int scale = 1;
                Bitmap result = new Bitmap(bitMatrix.Width * scale, bitMatrix.Height * scale);
                for (int x = 0; x < bitMatrix.Height; x++)
                {
                    for (int y = 0; y < bitMatrix.Width; y++)
                    {
                        Color pixel = bitMatrix[x, y] ? Color.Black : Color.White;
                        for (int i = 0; i < scale; i++)
                            for (int j = 0; j < scale; j++)
                                result.SetPixel(x * scale + i, y * scale + j, pixel);
                    }
                }
                System.IO.MemoryStream ms = new MemoryStream();
                result.Save(ms, ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                string sigBase64 = Convert.ToBase64String(byteImage);

                return sigBase64;

            } catch
            {
                return null;
            }
        }

        public async Task<JsonAnswerStatus> checkUserQRByAdmin(HttpContext httpContext, int id_of_user, string checkQR)
        {
            User user = await getCurrentOrNull(httpContext);
            if (user == null) return new JsonAnswerStatus("error", "not_auth");
            if(user.statusOfAdmin != 1) return new JsonAnswerStatus("error", "not_admin");

            UserService userService = new UserService(_dbc);
            User userForCheck = await userService.findById(id_of_user);
            if (userForCheck == null) return new JsonAnswerStatus("error", "not_found");
            if(userForCheck.checkQr != checkQR) return new JsonAnswerStatus("error", "wrong");

            return new JsonAnswerStatus("success", null);
        }



        public async Task<User> getCurrentOrNull(HttpContext httpContext)
        {
            UserService userService = new UserService(_dbc);
            if (httpContext.User.FindFirstValue(ClaimTypes.Role) != "User") return null;
            return await userService.findById(int.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public async Task<JsonAnswerStatus> checkStatusOfAdmin(HttpContext httpContext)
        {
            User user = await getCurrentOrNull(httpContext);
            if (user == null) return new JsonAnswerStatus("error", "not_auth");
            return (user.statusOfAdmin == 1
                ? new JsonAnswerStatus("success", null)
                : new JsonAnswerStatus("error", "denied")
            );
        }
    }
}
