namespace GiftCardManagementSystem.Admin.Models.User
{
    public class UserResponseModel : BaseResponseModel
    {
        public List<UserModel> UserList
        {
            get; set;
        }

    }
    public class UserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int MaxLimit { get; set; }
        public int CashbackPoint { get; set; }
        public decimal CashbackAmount { get; set; }
    }
}
