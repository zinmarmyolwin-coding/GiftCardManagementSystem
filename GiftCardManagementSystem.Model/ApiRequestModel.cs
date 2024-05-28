using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model
{
    public class ApiRequestModel
    {
        public object? ReqData { get; set; }
        public string UserId { get; set; }
        public string ServiceName { get; set; }
    }
}
