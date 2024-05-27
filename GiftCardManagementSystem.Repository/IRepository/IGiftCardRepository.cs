using GiftCardManagementSystem.Model.GiftCard;

namespace GiftCardManagementSystem.Repository.IRepository
{
    public interface IGiftCardRepository
    {
        GiftcardResponseModel GiftCardlist();
        GiftcardResponseModel GetByGiftCardId(int id);
        //void Add(Customer customer);
        //void Update(Customer customer);
    }
}
