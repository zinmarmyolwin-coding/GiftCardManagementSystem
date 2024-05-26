﻿namespace GiftCardManagementSystem.Admin.Models.Admin
{
    public class AdminUserRegisterRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
    public class SigninRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AdminUserResponseModel : BaseResponseModel
    {
    }
}