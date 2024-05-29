using GiftCardManagementSystem.Admin.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GiftCardManagementSystem.Admin.Features.Admin
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AdminList()
        {
            AdminUserResponseModel model = new AdminUserResponseModel();
            model = await _adminService.AdminList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AdminRegister()
        {
            var roleList = await UserRoleList(); // Assuming UserRoleList() method retrieves the list of roles
            ViewBag.RoleList = roleList;
            return View();
        }

        public async Task<UserRoleModel> UserRoleList()
        {
            var model = new UserRoleModel();
            model.Items = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Approver", Text = "Approver" },
                new SelectListItem { Value = "Manager", Text = "Manager" }
            };

            return model;
        }

        [HttpPost]
        public ActionResult AdminRegister(AdminUserRegisterRequestModel reqModel)
        {
            if (ModelState.IsValid)
            {
                var result = _adminService.AdminRegister(reqModel);
                return Redirect("AdminList");
            }
            return View(reqModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Signin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new SigninRequestModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Signin(SigninRequestModel reqModel)
        {
            var result = new AdminUserResponseModel();
            if (ModelState.IsValid)
            {
                result = _adminService.Signin(reqModel);
                if (result.Response.IsError)
                {
                    ViewBag.ErrorMessage = result.Response.RespDesp;
                    return View(reqModel);
                }
                return RedirectToAction("AdminList", "Admin");
            }
            return View(reqModel);
        }
    }
}
