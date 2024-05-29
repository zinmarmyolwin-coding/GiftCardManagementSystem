using GiftCardManagementSystem.Admin.Models;
using GiftCardManagementSystem.Admin.Models.User;
using GiftCardManagementSystem.Infrastructure.AppDbContextModels;
using static GiftCardManagementSystem.Admin.Models.User.UserResponseModel;

namespace GiftCardManagementSystem.Admin.Features.User
{
    public class UserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public UserResponseModel UserList()
        {
            var model = new UserResponseModel();
            try
            {
                var users = _db.TblUsers.ToList();

                model.UserList = users.ConvertAll(x => new UserModel
                {
                    UserId = x.UserId,
                    Name = x.Name,
                    Phone = x.PhoneNo,
                    MaxLimit = (int)x.MaximunLimit!,
                    CashbackPoint = (int)x.CashbackPoint!,
                    CashbackAmount = (decimal)x.CashbackAmount!,
                }).ToList();

                model.Response = SubResponseModel.SuccessResponse("Success", RespType.MS);
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.ME);
            }

            return model;
        }

        public UserResponseModel UserCreate(UserRequestModel reqModel)
        {
            var model = new UserResponseModel();
            try
            {
                bool isExist = _db.TblUsers.Any(x => x.PhoneNo == reqModel.Phone);

                if (isExist)
                {
                    model.Response = SubResponseModel.SuccessResponse("Dulplicate User.", RespType.ME);
                    goto Result;
                }

                var user = new TblUser()
                {
                    UserId = Guid.NewGuid().ToString(),
                    Name = reqModel.Name,
                    PhoneNo = reqModel.Phone,
                    MaximunLimit = reqModel.MaxLimit,
                    CashbackPoint = 0,
                    CashbackAmount = 0,
                };

                _db.TblUsers.Add(user);
                _db.SaveChanges();

                model.Response = SubResponseModel.SuccessResponse("Save Sucessful", RespType.MS);

            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.ME);
            }
        Result:
            return model;
        }
    }
}
