using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class CheckoutRequestModel : BaseSubRequestModel
    {
        public List<CheckoutModel> ckeckoutList { get; set; }
    }
}
