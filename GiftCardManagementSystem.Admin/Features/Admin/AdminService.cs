using GiftCardManagementSystem.Admin.Helper;
using GiftCardManagementSystem.Admin.Models;
using GiftCardManagementSystem.Admin.Models.Admin;
using GiftCardManagementSystem.Infrastructure.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GiftCardManagementSystem.Admin.Features.Admin
{
    public class AdminService
    {
        private readonly AppDbContext _db;

        public AdminService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AdminUserResponseModel> AdminList()
        {
            var model = new AdminUserResponseModel();
            try
            {
                var adminList = await _db.TblAdminusers.AsNoTracking().ToListAsync();

               model.AdminList = adminList.Select(x  => new AdminModel
               {
                  UserName = x.UserName,
                  Role = x.UserRole
               }).ToList();

                model.Response = SubResponseModel.SuccessResponse("Success",RespType.MS);
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.MS);
            }
            return model;
        }

        public AdminUserResponseModel AdminRegister(AdminUserRegisterRequestModel reqModel)
        {
            var model = new AdminUserResponseModel();
            try
            {
                string password = PasswordGenerate.SHA256HexHashString(reqModel.UserName, reqModel.Password);
                TblAdminuser adminuser = new TblAdminuser
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = reqModel.UserName,
                    Password = password,
                    UserRole = reqModel.UserRole,
                    DelFlag = false
                };

                _db.TblAdminusers.Add(adminuser);
                _db.SaveChanges();

                model.Response = SubResponseModel.SuccessResponse("Admin User Registration sucessfully.", RespType.MS);
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.MS);
            }
            return model;
        }

        public AdminUserResponseModel Signin(SigninRequestModel reqModel)
        {
            var model = new AdminUserResponseModel();
            try
            {
                string passwordHash = PasswordGenerate.SHA256HexHashString(reqModel.UserName, reqModel.Password);
                var adminUser = _db.TblAdminusers.FirstOrDefault(a => a.UserName == reqModel.UserName
                                              & a.Password == passwordHash
                                              & a.DelFlag == false);
                if (adminUser is null)
                {
                    model.Response = SubResponseModel.SuccessResponse("SignIn fail.", RespType.ME);
                    goto Result;
                }

                model.Response = SubResponseModel.SuccessResponse("SignIn sucessfully.", RespType.MS);
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.MS);
            }
        Result:
            return model;
        }
    }

}
