using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications;

public class ProductWithFiltersForCountSpec : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpec(ProductSpecParams productSpecParams) : base(Filter(productSpecParams))
    {

    }

    private static Expression<Func<Product, bool>> Filter(ProductSpecParams productSpecParams)
    {
        return p => (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
                    (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId) &&
                    (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search));
    }
}
