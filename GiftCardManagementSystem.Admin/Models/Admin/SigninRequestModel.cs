using System.ComponentModel.DataAnnotations;

namespace GiftCardManagementSystem.Admin.Models.Admin
{
    public class SigninRequestModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
