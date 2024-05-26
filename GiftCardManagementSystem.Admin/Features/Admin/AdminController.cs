using GiftCardManagementSystem.Admin.Models.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GiftCardManagementSystem.Admin.Features.Admin
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public ActionResult AdminRegister(AdminUserRegisterRequestModel reqModel)
        {
            var result = _adminService.AdminRegister(reqModel);
            return View(result);
        }

        [HttpPost]
        public ActionResult Signin(SigninRequestModel reqModel)
        {
            var result = _adminService.Signin(reqModel);
            return View(result);
        }
    }
}
