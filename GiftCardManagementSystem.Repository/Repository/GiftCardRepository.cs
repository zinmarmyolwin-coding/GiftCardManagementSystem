using GiftCardManagementSystem.DbService.AppDbContextModels;
using GiftCardManagementSystem.Model;
using GiftCardManagementSystem.Model.GiftCard;
using GiftCardManagementSystem.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Repository.Repository
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly AppDbContext _db;

        public GiftCardRepository(AppDbContext db)
        {
            _db = db;
        }

        public GiftcardResponseModel GiftCardlist()
        {
            var model = new GiftcardResponseModel();
            try
            {
                var giftcardList = _db.TblGiftcards.ToList().ConvertAll(x => new GiftcardModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    GiftCardNo = x.GiftCardNo,
                    ExpiryDate = x.ExpiryDate,
                    Amount = x.Amount,
                    Quantity = x.Quantity,
                    IsActive = x.IsActive,
                });

                model.GiftcardList = giftcardList;
                model.Response = SubResponseModel.GetResponseSuccess;

            }
            catch (Exception ex)
            {

                model.Response = SubResponseModel.GetResponseError(ex);
            }
            return model;

        }

        public GiftcardResponseModel GetByGiftCardId(int id)
        {
            var model = new GiftcardResponseModel();
            try
            {
                var giftcard = _db.TblGiftcards.Where(x => x.GiftCardId == id).Select(x => new GiftcardModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    GiftCardNo = x.GiftCardNo,
                    ExpiryDate = x.ExpiryDate,
                    Amount = x.Amount,
                    Quantity = x.Quantity,
                    IsActive = x.IsActive,
                }).FirstOrDefault()!;

                model.Giftcard = giftcard;
                model.Response = SubResponseModel.GetResponseSuccess;
            }
            catch (Exception ex)
            {

                model.Response = SubResponseModel.GetResponseError(ex);
            }
            return model;

        }
    }
}
