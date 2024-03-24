using Core.Entities;

namespace Core.Specifications;

public class IncludeTypesAndBrandsSpec : BaseSpecification<Product>
{
    public IncludeTypesAndBrandsSpec()
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }

    public IncludeTypesAndBrandsSpec(int id) : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductType);
        AddInclude(p => p.ProductBrand);
    }
}
