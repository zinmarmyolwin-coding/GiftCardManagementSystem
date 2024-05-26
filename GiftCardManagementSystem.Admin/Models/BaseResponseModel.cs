namespace GiftCardManagementSystem.Admin.Models
{
    public class BaseResponseModel
    {
        public ResponseModel Response { get; set; } = new ResponseModel();
    }
    public class ResponseModel
    {
        public string RespDesp { get; set; }
        public RespType RespType { get; set; }

        public bool IsSuccess => RespType == RespType.MS;
        public bool IsError => RespType == RespType.ME;
    }
    public enum RespType
    {
        MS, //Success
        MI, //Information
        MW, //Warning
        ME, //Error
        SE //Session Expired
    }

    public static class SubResponseModel
    {
        public static ResponseModel SuccessResponse(string respDesp, RespType msgType)
        {
            return new ResponseModel()
            {
                RespDesp = respDesp,
                RespType = msgType
            };
        }
    }
}
