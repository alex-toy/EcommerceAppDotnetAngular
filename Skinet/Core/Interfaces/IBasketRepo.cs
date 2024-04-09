using Core.Entities.Baskets;

namespace Core.Interfaces;

public interface IBasketRepo
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}
