using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardManagementSystem.Model.GiftCard
{
    public class GiftcardResponseModel : BaseSubResponseModel
    {
        public List<GiftcardModel> GiftcardList { get; set; }
        public GiftcardModel Giftcard { get; set; }
    }
}
