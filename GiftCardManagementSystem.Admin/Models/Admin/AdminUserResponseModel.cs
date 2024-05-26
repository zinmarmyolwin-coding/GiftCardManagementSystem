using System.ComponentModel.DataAnnotations;

namespace GiftCardManagementSystem.Admin.Models.Admin
{
    public class AdminUserResponseModel : BaseResponseModel
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string Password {  get; set; }
    }
}
