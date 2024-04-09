using Core.Entities.Products;
using System.Linq.Expressions;

namespace Core.Specifications;

public class IncludeTypesAndBrandsSpec : BaseSpecification<Product>
{
    public IncludeTypesAndBrandsSpec(ProductSpecParams productSpecParams) : base(Filter(productSpecParams))
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
        AddOrderBy(p => p.Name);
        ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

        if (!string.IsNullOrEmpty(productSpecParams.Sort))
        {
            switch (productSpecParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }

    public IncludeTypesAndBrandsSpec(int id) : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }

    private static Expression<Func<Product, bool>> Filter(ProductSpecParams productSpecParams)
    {
        return p => (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) && 
                    (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId) && 
                    (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search));
    }
}
