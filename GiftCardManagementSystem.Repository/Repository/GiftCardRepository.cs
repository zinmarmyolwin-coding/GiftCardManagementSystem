using GiftCardManagementSystem.Infrastructure.AppDbContextModels;
using GiftCardManagementSystem.Model;
using GiftCardManagementSystem.Model.GiftCard;
using GiftCardManagementSystem.Repository.Enum;
using GiftCardManagementSystem.Repository.IRepository;
using GiftCardManagementSystem.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<GiftcardResponseModel> GiftCardlistAsync()
        {
            var model = new GiftcardResponseModel();
            try
            {
                var giftcardList = await _db.TblGiftcards.ToListAsync();
                model.GiftcardList = giftcardList.ConvertAll(x => new GiftcardModel
                {
                    GiftCardId = x.GiftCardId,
                    Title = x.Title,
                    Description = x.Description,
                    GiftCardNo = x.GiftCardNo,
                    ExpiryDate = x.ExpiryDate,
                    Amount = x.Amount,
                    Quantity = x.Quantity,
                    CashbackPoint = (int)x.CashbackPoint!,
                    CashbackAmount = (decimal)x.CashbackAmount!,
                    IsActive = x.IsActive,
                    IsOutOfStock = x.Quantity == 0 ? true : false,
                });
                model.Response = SubResponseModel.GetResponseSuccess;
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.GetResponseError(ex);
            }
            return model;
        }

        public async Task<GiftcardResponseModel> GetByGiftCardIdAsync(string raw)
        {
            var reqModel = raw.ToObject<GiftcardRequestModel>();
            int id = reqModel.Id;

            var model = new GiftcardResponseModel();
            try
            {
                var giftcard = await _db.TblGiftcards.Where(x => x.GiftCardId == id).Select(x => new GiftcardModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    GiftCardNo = x.GiftCardNo,
                    ExpiryDate = x.ExpiryDate,
                    Amount = x.Amount,
                    Quantity = x.Quantity,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();

                model.Giftcard = giftcard;
                model.Response = SubResponseModel.GetResponseSuccess;
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.GetResponseError(ex);
            }
            return model;
        }

        public async Task<CheckoutResponseModel> CheckoutListAsync(string raw)
        {
            var model = new CheckoutResponseModel();
            try
            {
                CheckoutRequestModel reqModel = raw.ToObject<CheckoutRequestModel>();
                if (reqModel.ckeckoutList.Count == 0)
                {
                    return model;
                }

                List<CheckoutModel> checkoutList = new List<CheckoutModel>();
                foreach (var item in reqModel.ckeckoutList)
                {
                    var giftcard = await _db.TblGiftcards.FirstOrDefaultAsync(x => x.GiftCardNo == item.GiftCardCode);

                    if (giftcard != null)
                    {
                        int? cashbackPoint = (item.Quantity * giftcard.CashbackPoint);
                        var checkout = new CheckoutModel
                        {
                            GiftCardCode = item.GiftCardCode,
                            Name = item.Name,
                            Quantity = item.Quantity,
                            Amount = item.Amount,
                            TotalAmount = item.Quantity * item.Amount,
                            CashbackPoint = cashbackPoint,
                            CashbackAmount = (cashbackPoint * giftcard.CashbackAmount)
                        };
                        checkoutList.Add(checkout);
                    }
                }

                decimal totalAmount = checkoutList.Sum(x => x.TotalAmount);
                decimal? cashBackAmount = checkoutList.Sum(x => x.CashbackAmount);
                decimal? cashBackPoint = checkoutList.Sum(x => x.CashbackPoint);

                string userId = reqModel.UserId;
                using (var tran = await _db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var tranId = Ulid.NewUlid().ToString();
                        TblTransactionhistory tblTransactionhistory = new TblTransactionhistory()
                        {
                            TranId = tranId,
                            UserId = userId,
                            CreatedDate = DateTime.Now,
                            TotalAmount = totalAmount,
                            CashbackAmount = cashBackAmount,
                            Status = EumTransactionStatus.Process.ToString()
                        };

                        await _db.TblTransactionhistories.AddAsync(tblTransactionhistory);
                        await _db.SaveChangesAsync();

                        foreach (var item in reqModel.ckeckoutList)
                        {
                            TblTransactionhistorydetail tblTransactionhistorydetail = new TblTransactionhistorydetail();
                            tblTransactionhistorydetail.TranId = tranId;
                            tblTransactionhistorydetail.GiftCardNo = item.GiftCardCode;
                            tblTransactionhistorydetail.UserId = userId;
                            tblTransactionhistorydetail.CreatedDate = DateTime.Now;
                            tblTransactionhistorydetail.Amount = item.Amount;
                            tblTransactionhistorydetail.Quantity = item.Quantity;
                            tblTransactionhistorydetail.TotalAmount = (item.Quantity * item.Amount);
                            tblTransactionhistorydetail.Status = EumTransactionStatus.Process.ToString();

                            await _db.TblTransactionhistorydetails.AddAsync(tblTransactionhistorydetail);
                            await _db.SaveChangesAsync();
                        }

                        await tran.CommitAsync();

                        model.ckeckoutList = checkoutList;
                        model.TotalAmount = totalAmount;
                        model.CashbackAmount = (decimal)cashBackAmount!;
                        model.CashbackPoint = (int)cashBackPoint!;
                        model.TranId = tranId;
                        model.Response = SubResponseModel.GetResponseSuccess;
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        model.Response = SubResponseModel.GetResponseError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.GetResponseError(ex);
            }
            return model;
        }

        public async Task<PaymentMethodResponseModel> PaymentMethodListAsync()
        {
            var model = new PaymentMethodResponseModel();
            try
            {
                var paymentMethods = await _db.TblPaymentmethods.Where(x => x.Active == true).ToListAsync();
                model.PaymentMethodList = paymentMethods.Select(x => new PaymentMethodModel
                {
                    PaymentMethodCode = x.PaymentMethodCode,
                    PaymentMethodName = x.PaymentMethodName,
                    Discount = (decimal)x.Discount!
                }).ToList();

                model.Response = SubResponseModel.GetResponseSuccess;
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.GetResponseError(ex);
            }
            return model;
        }

        public async Task<PaymentResponseModel> ConfirmPaymentAsync(string raw)
        {
            var model = new PaymentResponseModel();

            try
            {
                decimal? cashbackAmount = decimal.Zero;
                decimal? discountAmount = decimal.Zero;
                bool? isUsedCashback = false;

                var reqModel = raw.ToObject<PaymentRequestModel>();
                decimal? totalAmount = reqModel.TotalAmount;

                var user = await _db.TblUsers.FirstOrDefaultAsync(x => x.UserId == reqModel.UserId);
                if (user is null) goto Result;

                if (user.MaximunLimit <= 0)
                {
                    model.Response = SubResponseModel.GetResponse("This user has exceeded max Limit.");
                    goto Result;
                }

                var paymentMethod = await _db.TblPaymentmethods.FirstOrDefaultAsync(x => x.PaymentMethodCode == reqModel.PaymentCode
                    && x.Active == true);
                if (paymentMethod == null) goto Result;

                if (user.CashbackPoint > 0)
                {
                    isUsedCashback = true;
                    cashbackAmount = user.CashbackAmount;
                }

                if (paymentMethod.PaymentMethodCode == EumPaymentMethod.KPAY.ToString())
                {
                    discountAmount = paymentMethod.Discount % 100;
                }

                decimal? netAmount = reqModel.TotalAmount - (cashbackAmount + discountAmount);

                var tbltransactionHistory = await _db.TblTransactionhistories.FirstOrDefaultAsync(x => x.TranId == reqModel.TranId);

                if (tbltransactionHistory is null) goto Result;

                using (var tran = await _db.Database.BeginTransactionAsync())
                {
                    try
                    {
                        #region Update TransactionHistory

                        tbltransactionHistory.TransactionDate = DateTime.Now;
                        tbltransactionHistory.NetAmount = netAmount;
                        tbltransactionHistory.DiscountAmount = discountAmount;
                        tbltransactionHistory.PaymentCode = reqModel.PaymentCode;
                        tbltransactionHistory.IsUsedCahback = (bool)isUsedCashback;
                        tbltransactionHistory.Status = EumTransactionStatus.Paid.ToString();

                        _db.TblTransactionhistories.Update(tbltransactionHistory);
                        await _db.SaveChangesAsync();

                        #endregion

                        #region Update Transactionhistorydetail

                        var tranHistoryDetail = await _db.TblTransactionhistorydetails.Where(x => x.TranId == reqModel.TranId).ToListAsync();

                        foreach (var item in tranHistoryDetail)
                        {
                            item.ToUserId = reqModel.ToUser;
                            item.IsSelfService = reqModel.IsSelf;
                            item.TransactionDate = DateTime.Now;
                            item.Status = EumTransactionStatus.Paid.ToString();

                            _db.TblTransactionhistorydetails.Update(item);
                            await _db.SaveChangesAsync();
                        }

                        #endregion

                        #region Update GiftCard

                        foreach (var item in tranHistoryDetail)
                        {
                            var giftcard = await _db.TblGiftcards.FirstOrDefaultAsync(x => x.GiftCardNo == item.GiftCardNo);

                            giftcard.Quantity =(int) (giftcard.Quantity - item.Quantity!);
                            _db.TblGiftcards.Update(giftcard);
                            await _db.SaveChangesAsync();
                        }
                        #endregion

                        #region Update User

                        user.MaximunLimit =( user.MaximunLimit - 1);
                        user.CashbackAmount = reqModel.CashbackAmount;
                        user.CashbackPoint = reqModel.CashbackPoint;
                        _db.TblUsers.Update(user);
                        await _db.SaveChangesAsync();

                        #endregion



                        await tran.CommitAsync();

                        model.Response = SubResponseModel.GetResponse("Your Transaction has been successful.");
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        model.Response = SubResponseModel.GetResponseError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.GetResponseError(ex);
            }

        Result:
            return model;
        }

        public async Task<TranHistoryResponseModel> PurchaseHistoryAsync(string raw)
        {
            var model = new TranHistoryResponseModel();
            try
            {
                var reqModel = raw.ToObject<TranHistoryRequestModel>();

                bool isUsedCashback = reqModel.Cashback == EumCashback.UsedCashback.ToString();

                var tranHistory = from transaction in _db.TblTransactionhistories
                                  join user in _db.TblUsers on transaction.UserId equals user.UserId into userGroup
                                  from user in userGroup.DefaultIfEmpty()
                                  join payment in _db.TblPaymentmethods on transaction.PaymentCode
                                  equals payment.PaymentMethodCode into paymentGroup
                                  from payment in paymentGroup.DefaultIfEmpty()
                                  where transaction.IsUsedCahback == isUsedCashback
                                  select new TranHistoryModel
                                  {
                                      TranId = transaction.TranId!,
                                      TransactionDate = transaction.TransactionDate.ToString()!,
                                      TotalAmount = (decimal)transaction.TotalAmount!,
                                      CashbackAmount = (decimal)transaction.CashbackAmount!,
                                      NetAmount = (decimal)transaction.NetAmount!,
                                      DiscountAmount = (decimal)transaction.DiscountAmount!,
                                      UserName = user != null ? user.Name : "",
                                      PaymentName = payment != null ? payment.PaymentMethodName : "",
                                  };

                model.TranHistoryList = await tranHistory.ToListAsync();
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