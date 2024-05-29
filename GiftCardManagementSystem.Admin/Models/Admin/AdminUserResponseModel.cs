using System.ComponentModel.DataAnnotations;

namespace GiftCardManagementSystem.Admin.Models.Admin
{
    public class AdminUserResponseModel : BaseResponseModel
    {
       public List<AdminModel> AdminList { get; set; }
    }

    public class AdminModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string No { get; set; }
    }
}
