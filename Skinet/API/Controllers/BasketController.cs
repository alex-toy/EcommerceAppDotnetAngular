using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepo _basketRepo;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepo basketRepository, IMapper mapper)
    {
        _mapper = mapper;
        _basketRepo = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await _basketRepo.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    {
        CustomerBasket customerBasket = _mapper.Map<CustomerBasket>(basket);

        CustomerBasket updatedBasket = await _basketRepo.UpdateBasketAsync(customerBasket);

        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await _basketRepo.DeleteBasketAsync(id);
    }
}
