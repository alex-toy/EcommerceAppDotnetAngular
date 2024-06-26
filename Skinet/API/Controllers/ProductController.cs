﻿using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities.Products;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductController : BaseApiController
{
    private readonly IGenericRepo<Product> _productRepo;
    private readonly IGenericRepo<ProductType> _productTypeRepo;
    private readonly IGenericRepo<ProductBrand> _productBrandRepo;
    private readonly IMapper _mapper;

    public ProductController(IGenericRepo<Product> productRepo, IGenericRepo<ProductType> productTypeRepo, IGenericRepo<ProductBrand> productBrandRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _productTypeRepo = productTypeRepo;
        _productBrandRepo = productBrandRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
    {
        var countSpec = new ProductWithFiltersForCountSpec(productSpecParams);
        int totalItems = await _productRepo.CountAsync(countSpec);
        IReadOnlyList<Product> products = await _productRepo.ListAsync(new IncludeTypesAndBrandsSpec(productSpecParams));
        IReadOnlyList<ProductToReturnDto> data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
        return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        Product product = await _productRepo.GetEntityWithSpec(new IncludeTypesAndBrandsSpec(id));

        if (product is null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<ProductToReturnDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        var products = await _productBrandRepo.ListAllAsync();
        return Ok(products);
    }

    [HttpGet("brand/{id}")]
    public async Task<ActionResult<ProductBrand>> GetproductBrand(int id)
    {
        return await _productBrandRepo.GetByIdAsync(id);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        var products = await _productTypeRepo.ListAllAsync();
        return Ok(products);
    }

    [HttpGet("type/{id}")]
    public async Task<ActionResult<ProductType>> GetproductType(int id)
    {
        return await _productTypeRepo.GetByIdAsync(id);
    }
}
