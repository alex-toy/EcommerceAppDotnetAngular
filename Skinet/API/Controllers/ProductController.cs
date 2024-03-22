using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IGenericRepo<Product> _productRepo;
    private readonly IGenericRepo<ProductType> _productTypeRepo;
    private readonly IGenericRepo<ProductBrand> _productBrandRepo;

    public ProductController(IGenericRepo<Product> productRepo, IGenericRepo<ProductType> productTypeRepo, IGenericRepo<ProductBrand> productBrandRepo)
    {
        _productRepo = productRepo;
        _productTypeRepo = productTypeRepo;
        _productBrandRepo = productBrandRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _productRepo.ListAllAsync();
        return Ok(products);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        var products = await _productBrandRepo.ListAllAsync();
        return Ok(products);
    }

    [HttpGet("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        var products = await _productTypeRepo.ListAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        return await _productRepo.GetByIdAsync(id);
    }

    [HttpGet("brand/{id}")]
    public async Task<ActionResult<ProductBrand>> GetproductBrand(int id)
    {
        return await _productBrandRepo.GetByIdAsync(id);
    }

    [HttpGet("type/{id}")]
    public async Task<ActionResult<ProductType>> GetproductType(int id)
    {
        return await _productTypeRepo.GetByIdAsync(id);
    }
}
