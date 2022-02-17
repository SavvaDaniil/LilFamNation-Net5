using LilFamStudioNet5.Data;
using LilFamStudioNet5.DTO;
using LilFamStudioNet5.DTO.Admin;
using LilFamStudioNet5.Entities;
using LilFamStudioNet5.Models.Admin;
using LilFamStudioNet5.Services;
using LilFamStudioNet5.ViewModels;
using LilFamStudioNet5.ViewModels.Admin;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LilFamStudioNet5.Facades
{
    public class AdminFacade
    {
        public ApplicationDbContext _dbc;
        public AdminFacade(ApplicationDbContext dbc)
        {
            this._dbc = dbc;
        }

        public async Task<JsonAnswerStatus> add(AdminNewDTO adminNewDTO)
        {
            AdminService adminService = new AdminService(_dbc);

            if (adminService.isAlreadyExistByUsername(adminNewDTO.username))
            {
                return new JsonAnswerStatus("error", "username_already_exist");
            }

            Admin admin = await adminService.add(adminNewDTO.username);
            if (admin == null) return new JsonAnswerStatus("error", "unknown");

            return new JsonAnswerStatus("success", null);
        }

        public async Task<JsonAnswerStatus> editUpdate(AdminEditDTO adminEditDTO)
        {
            AdminService adminService = new AdminService(_dbc);
            Admin admin = await adminService.findById(adminEditDTO.id_of_admin);
            if (admin == null) return new JsonAnswerStatus("error", "not_found");

            if(admin.Username != adminEditDTO.username)
            {
                if (adminService.isAlreadyExistByUsernameExceptId(admin.id, adminEditDTO.username))
                {
                    return new JsonAnswerStatus("error", "username_already_exist");
                }
            }

            string newPassword = null;
            if(adminEditDTO.new_password != null)
            {
                newPassword = BCrypt.Net.BCrypt.HashPassword(adminEditDTO.new_password);
            }

            if (await adminService.updateEdit(
                admin,
                adminEditDTO.username,
                adminEditDTO.name,
                adminEditDTO.position,
                newPassword,
                adminEditDTO.active,
                adminEditDTO.panelAdmins,
                adminEditDTO.panelLessons,
                adminEditDTO.panelUsers,
                adminEditDTO.panelDanceGroups,
                adminEditDTO.panelTeachers,
                adminEditDTO.panelAbonements,
                adminEditDTO.panelDiscounts,
                adminEditDTO.panelBranches
            ))
            {
                return new JsonAnswerStatus("success", null);
            }

            return new JsonAnswerStatus("error", "unknown");
        }


        public async Task<JsonAnswerStatus> search(AdminSearchDTO adminSearchDTO)
        {
            AdminService adminService = new AdminService(_dbc);
            List<Admin> adminsSearchResult = await adminService.searchAdmins(adminSearchDTO);
            int searchCount = adminService.searchCount(adminSearchDTO);

            List<AdminPreviewLiteViewModel> adminPreviewLiteViewModels = new List<AdminPreviewLiteViewModel>();
            foreach (Admin admin in adminsSearchResult)
            {
                adminPreviewLiteViewModels.Add(new AdminPreviewLiteViewModel(
                    admin.id,
                    admin.Username,
                    admin.position,
                    admin.active
                    )
                );
            }

            return new JsonAnswerStatus("success", null, new AdminSearchViewModel(adminPreviewLiteViewModels, searchCount));
        }


        public async Task<AdminEditViewModel> get(int id)
        {
            AdminService adminService = new AdminService(_dbc);
            Admin admin = await adminService.findById(id);
            if (admin == null) return null;

            List<AdminPanelAccessLiteViewModel> adminPanelAccessLiteViewModels = new List<AdminPanelAccessLiteViewModel>();

            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelLessons", admin.panelLessons, "Занятия"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelUsers", admin.panelUsers, "Пользователи"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelDanceGroups", admin.panelDanceGroups, "Группы"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelTeachers", admin.panelTeachers, "Преподаватели"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelAbonements", admin.panelAbonements, "Абонементы"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelDiscounts", admin.panelDiscounts, "Скидки"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelBranches", admin.panelBranches, "Филиалы"));
            adminPanelAccessLiteViewModels.Add(new AdminPanelAccessLiteViewModel("panelAdmins", admin.panelAdmins, "Администраторы"));


            return new AdminEditViewModel(
                admin.id,
                admin.Username,
                admin.active,
                admin.position,
                adminPanelAccessLiteViewModels
            );
        }




        public async Task<JsonAnswerStatus> delete(AdminIdDTO adminIdDTO)
        {
            AdminService adminService = new AdminService(_dbc);

            return (await adminService.delete(adminIdDTO.id_of_admin)
                ? new JsonAnswerStatus("success", null)
                : new JsonAnswerStatus("error", "unknown")
            );
        }


        public async Task<Admin> login(AdminLoginDTO adminLoginDTO)
        {
            AdminService adminService = new AdminService(_dbc);
            Admin admin = await adminService.findByUsername(adminLoginDTO.username);
            if (admin == null) return null;

            if (BCrypt.Net.BCrypt.Verify(adminLoginDTO.password, admin.Password)) return admin;
            return null;
        }

        public async Task<AdminAuthorizeViewModel> getAuthorizeViewModel(HttpContext httpContext, string menuActive = null)
        {
            Admin admin = await getCurrentOrNull(httpContext);
            if (admin == null) return new AdminAuthorizeViewModel("", 0, null);
            List<string> listOfAvailablePanels = new List<string>();
            if (admin.panelAdmins >= 1) listOfAvailablePanels.Add("panelAdmins");
            if (admin.panelLessons >= 1) listOfAvailablePanels.Add("panelLessons");
            if (admin.panelUsers >= 1) listOfAvailablePanels.Add("panelUsers");
            if (admin.panelDanceGroups >= 1) listOfAvailablePanels.Add("panelDanceGroups");
            if (admin.panelTeachers >= 1) listOfAvailablePanels.Add("panelTeachers");
            if (admin.panelAbonements >= 1) listOfAvailablePanels.Add("panelAbonements");
            if (admin.panelDiscounts >= 1) listOfAvailablePanels.Add("panelDiscounts");
            if (admin.panelBranches >= 1) listOfAvailablePanels.Add("panelBranches");

            return new AdminAuthorizeViewModel(menuActive, admin.level, listOfAvailablePanels);
        }

        public async Task<AdminProfileViewModel> getCurrentProfile(HttpContext httpContext)
        {
            Admin admin = await this.getCurrentOrNull(httpContext);
            if (admin == null) return null;
            return new AdminProfileViewModel(
                admin.id,
                admin.name,
                admin.Username,
                admin.position
            );
        }

        public async Task<AdminSaveProfileResult> update(HttpContext httpContext, AdminProfileDTO adminProfileDTO)
        {
            AdminService adminService = new AdminService(_dbc);
            Admin admin = await this.getCurrentOrNull(httpContext);
            if (admin == null) return null;

            if (adminProfileDTO.username != admin.Username)
            {
                if (adminService.isAlreadyExistByUsernameExceptId(admin.id, adminProfileDTO.username))
                {
                    return new AdminSaveProfileResult("error", "username_already_exist");
                }
            }

            AdminSaveProfileResult adminSaveProfileResult = new AdminSaveProfileResult(admin);
            if (adminProfileDTO.password != null || adminProfileDTO.username != admin.Username) adminSaveProfileResult.isNeedRelogin = true;

            if (await adminService.update(
                    admin,
                    adminProfileDTO.username,
                    adminProfileDTO.name,
                    adminProfileDTO.position,
                    adminProfileDTO.password
                )
            )
            {
                adminSaveProfileResult.status = "success";
            } else
            {
                adminSaveProfileResult.status = "error";
            }

            return adminSaveProfileResult;
        }

        public async Task checkExistRoot()
        {
            AdminService adminService = new AdminService(_dbc);
            Admin admin = await adminService.findByUsername("admin2");
            if (admin != null) return;

            await adminService.add("admin2", "123");
        }

        public async Task<Admin> getCurrentOrNull(HttpContext httpContext)
        {
            AdminService adminService = new AdminService(_dbc);
            if (httpContext.User.FindFirstValue(ClaimTypes.Role) != "Admin") return null;
            return await adminService.findById(int.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }
    }
}
