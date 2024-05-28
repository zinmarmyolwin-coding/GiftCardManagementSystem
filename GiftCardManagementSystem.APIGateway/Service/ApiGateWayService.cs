using GiftCardManagementSystem.Model;
using GiftCardManagementSystem.Repository.Enum;
using GiftCardManagementSystem.Repository.IRepository;
using GiftCardManagementSystem.Repository.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GiftCardManagementSystem.APIGateway.Service
{
    public class ApiGateWayService
    {
        private readonly IGiftCardRepository _giftCardRepository;

        public ApiGateWayService(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        public async Task<object> Service(ApiRequestModel reqModel)
        {
            string serviceName = reqModel.ServiceName;

            string raw = reqModel.ReqData!.ToString().ToJson()!;

            if (!raw.IsNullOrEmpty())
            {
                var rawObj = JObject.Parse(raw);

                if (!string.IsNullOrEmpty(reqModel.UserId))
                {
                    rawObj["UserId"] = reqModel.UserId;
                }

                raw = rawObj.ToString();

            }

            object? result = null;
            var name = ToEnum<EumServiceName>(serviceName);
            result = name switch
            {
                EumServiceName.GiftCardlist => await _giftCardRepository.GiftCardlistAsync(),
                EumServiceName.GetByGiftCardId => await _giftCardRepository.GetByGiftCardIdAsync(raw),
                EumServiceName.CheckoutList => await _giftCardRepository.CheckoutListAsync(raw),
                EumServiceName.PaymentMethodList => await _giftCardRepository.PaymentMethodListAsync(),
                EumServiceName.ConfirmPayment => await _giftCardRepository.ConfirmPaymentAsync(raw),
                EumServiceName.PurchaseHistory => await _giftCardRepository.PurchaseHistoryAsync(raw),
                _ => throw new Exception("Invalid Request Service")
            };
            return result;
        }

        public T ToEnum<T>(string enumString)
        {
            T enumValue = (T)Enum.Parse(typeof(T), enumString);
            return enumValue;
        }
    }

}
