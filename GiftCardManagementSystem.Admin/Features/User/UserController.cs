using GiftCardManagementSystem.Admin.Features.Payment;
using GiftCardManagementSystem.Admin.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace GiftCardManagementSystem.Admin.Features.User
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var model = new UserResponseModel();
            model = _userService.UserList();
            return View(model);
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserCreate(UserRequestModel reqModel)
        {
            if (!ModelState.IsValid)
            {
                var result = _userService.UserCreate(reqModel);
                if (result.Response.IsError)
                {
                    ViewBag.ErrorMessage = result.Response.RespDesp;
                    goto Result;
                }
                return RedirectToAction("UserList", "User");

            }
        Result:
            return View(reqModel);
        }
    }
}
