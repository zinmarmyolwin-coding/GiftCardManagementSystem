using System.ComponentModel.DataAnnotations;

namespace GiftCardManagementSystem.Admin.Models.User
{
    public class UserRequestModel 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public int MaxLimit { get; set; }
    }
}
