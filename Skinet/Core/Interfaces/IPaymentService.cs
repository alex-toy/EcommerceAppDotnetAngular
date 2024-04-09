using Core.Entities.Baskets;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
         Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
         Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
         Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}