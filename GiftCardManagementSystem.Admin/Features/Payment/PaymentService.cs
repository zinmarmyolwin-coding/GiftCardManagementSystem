﻿using GiftCardManagementSystem.Admin.Helper;
using GiftCardManagementSystem.Admin.Models.Admin;
using GiftCardManagementSystem.Admin.Models;
using GiftCardManagementSystem.Admin.Models.GiftCard;
using GiftCardManagementSystem.DbService.AppDbContextModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using GiftCardManagementSystem.Admin.Models.Payment;

namespace GiftCardManagementSystem.Admin.Features.Payment
{
    [Authorize]
    public class PaymentService
    {
        private readonly AppDbContext _db;

        public PaymentService(AppDbContext db)
        {
            _db = db;
        }

        public PaymentResponseModel PaymentList()
        {
            var model = new PaymentResponseModel();
            try
            {
                var payments = _db.TblPaymentmethods.ToList();

                model.PaymentList = payments.ConvertAll(x => new PaymentModel
                {
                    PaymentMethodName = x.PaymentMethodName,
                    Discount = (decimal)x.Discount,
                }).ToList();

                model.Response = SubResponseModel.SuccessResponse("Success", RespType.MS);
            }
            catch (Exception ex)
            {
                model.Response = SubResponseModel.SuccessResponse(ex?.Message, RespType.ME);
            }

            return model;
        }

        public PaymentResponseModel PaymentCreate(PaymentRequestModel reqModel)
        {
            var model = new PaymentResponseModel();
            try
            {
                bool isExist = _db.TblPaymentmethods.Any(x => x.PaymentMethodName == reqModel.PaymentMethodName);

                if (true)
                {
                    model.Response = SubResponseModel.SuccessResponse("Dulplicate Paymethod Name.", RespType.MS);
                    goto Result;
                }

                TblPaymentmethod paymentmethod = new TblPaymentmethod()
                {
                    PaymentMethodName = reqModel.PaymentMethodName,
                    Discount = reqModel.Discount,
                };

                _db.TblPaymentmethods.Add(paymentmethod);
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