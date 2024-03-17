using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    public string GetProducts()
    {
        return "this will a list of products";
    }

    [HttpGet("{id}")]
    public string GetProduct(int id)
    {
        return "this will aproducts";
    }
}
