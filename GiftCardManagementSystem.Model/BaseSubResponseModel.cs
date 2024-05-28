using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model
{
    public class BaseSubResponseModel
    {
        public ResponseModel Response { get; set; } = new ResponseModel();
    }
    public class ResponseModel
    {
        public string RespCode { get; set; }
        public string RespDesp { get; set; }
        public EnumRespType RespType { get; set; }
        public bool IsSuccess => RespType == EnumRespType.MS;
        public bool IsError => !IsSuccess;
    }
    public enum EnumRespType
    {
        MS,
        MI,
        MW,
        ME,
        SE,
    }

    public static class SubResponseModel
    {
        public static ResponseModel GetResponseSuccess =>
          new ResponseModel { RespCode = "MS#000", RespDesp = "Success", RespType = EnumRespType.MS };

        public static ResponseModel GetResponseError(this Exception ex)
        {
            return new ResponseModel
            {
                RespCode = "ME#000",
                RespDesp = ex.ToString(),
                RespType = EnumRespType.ME
            };
        }

        public static ResponseModel GetResponse(string message)
        {
            return new ResponseModel()
            {
                RespDesp = message,
            };
        }
    }
}
