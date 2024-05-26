using GiftCardManagementSystem.Admin.Helper;
using GiftCardManagementSystem.Admin.Models.Admin;
using GiftCardManagementSystem.Admin.Models;
using GiftCardManagementSystem.Admin.Models.GiftCard;
using GiftCardManagementSystem.DbService.AppDbContextModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace GiftCardManagementSystem.Admin.Features.GiftCard
{
    [Authorize]
    public class PaymentService
    {
        private readonly AppDbContext _db;

        public PaymentService(AppDbContext db)
        {
            _db = db;
        }

        public GiftCardResponseModel GiftCardList()
        {
            var model = new GiftCardResponseModel();
            try
            {
                var giftcards = _db.TblGiftcards.Where(x => x.IsActive == true).ToList();
                model.GiftCardList = giftcards.ConvertAll(x => new GiftCardModel
                {
                    GiftCardId = x.GiftCardId,
                    Title = x.Title,
                    Description = x.Description,
                    GiftCardNo = x.GiftCardNo,
                    ExpiryDate = x.ExpiryDate.ToString(),
                    Amount = x.Amount,
                    Quantity = x.Quantity,

                }).ToList();

                model.Response = SubResponseModel.SuccessResponse("Success", RespType.MS);
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.ME);
            }

            return model;
        }

        public GiftCardResponseModel GiftCardCreate(GiftCardRequestModel reqModel)
        {
            var model = new GiftCardResponseModel();
            try
            {
                bool isExist = _db.TblGiftcards.Any(x => x.Title == reqModel.Title
                && x.IsActive == true);

                if (true)
                {
                    model.Response = SubResponseModel.SuccessResponse("Dulplicate GiftCard Number.", RespType.MS);
                    goto Result;
                }

                TblGiftcard giftcard = new TblGiftcard()
                {
                    Title = reqModel.Title,
                    Description = reqModel.Description,
                    GiftCardNo = Ulid.NewUlid().ToString(),
                    ExpiryDate = Convert.ToDateTime(reqModel.ExpiryDate),
                    Amount = reqModel.Amount,
                    Quantity = reqModel.Quantity,
                    CreatedDate = reqModel.CreatedDate,
                    CreatedUserId = reqModel.CreatedUserId,
                    IsActive = true,
                };

                _db.TblGiftcards.Add(giftcard);
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

        public GiftCardResponseModel GiftCardById(GiftCardRequestModel reqModel)
        {
            var model = new GiftCardResponseModel();
            try
            {
                var giftCard = _db.TblGiftcards.FirstOrDefault(x => x.GiftCardId == reqModel.GiftCardId
                && x.IsActive == true);
                
                if (giftCard is null) 
                {
                    model.Response = SubResponseModel.SuccessResponse("GiftCard Not found.", RespType.MS);
                    goto Result;
                }

                model.GiftCard = new GiftCardModel
                {
                    Title = giftCard.Title,
                    Description = giftCard.Description,
                    ExpiryDate = reqModel.ExpiryDate.ToString(),
                    Amount = reqModel.Amount,
                    Quantity = reqModel.Quantity,
                    IsActive =(bool)reqModel.IsActive,
                };
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.ME);
            }
        Result:
            return model;
        }

        public GiftCardResponseModel GiftCardEdit(GiftCardRequestModel reqModel)
        {
            var model = new GiftCardResponseModel();
            try
            {
                TblGiftcard? giftcard = _db.TblGiftcards.FirstOrDefault(x => x.GiftCardId == reqModel.GiftCardId 
                && x.IsActive == false);

                bool isExist = _db.TblGiftcards.Any(x => x.Title == reqModel.Title
                && x.IsActive == true && x.GiftCardId != reqModel.GiftCardId);

                if (true)
                {
                    model.Response = SubResponseModel.SuccessResponse("Dulplicate GiftCard Number.", RespType.MS);
                    goto Result;
                }

                giftcard.Title = reqModel.Title;
                giftcard.Description = reqModel.Description;
                giftcard.ExpiryDate = Convert.ToDateTime(reqModel.ExpiryDate);
                giftcard.Amount = reqModel.Amount;
                giftcard.Quantity = reqModel.Quantity;
                giftcard.IsActive = reqModel.IsActive;
                giftcard.UpdatedDate = DateTime.Now;
                giftcard.UpdatedUserId = reqModel.UpdatedUserId;

                _db.TblGiftcards.Update(giftcard);
                _db.SaveChanges();

                model.Response = SubResponseModel.SuccessResponse("Update Sucessful", RespType.MS);

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
