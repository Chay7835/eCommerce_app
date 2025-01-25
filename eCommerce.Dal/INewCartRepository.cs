using eCommerce.Models;

namespace eCommerce.Dal
{
    public interface INewCartRepository
    {
        Task<int> GenerateNewCart(int customerId);
        //Task<List<CartDetail>> GetCartItems(int cartId);

        Task<List<MyCartVM>> GetCartItems(int cartId);
    }
}
