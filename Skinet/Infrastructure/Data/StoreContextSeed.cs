using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedDataAsync(StoreContext context)
    {
        await AddProductBrands(context);
        await AddProductTypes(context);
        await AddProducts(context);

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }

    private static async Task AddProductBrands(StoreContext context)
    {
        if (!context.ProductBrands.Any())
        {
            string data = File.ReadAllText("..\\..\\SeedData\\brands.json");
            List<ProductBrand>? entities = JsonSerializer.Deserialize<List<ProductBrand>>(data);
            context.ProductBrands.AddRange(entities!);
            await context.SaveChangesAsync();
        }
    }

    private static async Task AddProductTypes(StoreContext context)
    {
        if (!context.ProductTypes.Any())
        {
            string data = File.ReadAllText("..\\..\\SeedData\\types.json");
            List<ProductType>? entities = JsonSerializer.Deserialize<List<ProductType>>(data);
            context.ProductTypes.AddRange(entities!);
            await context.SaveChangesAsync();
        }
    }

    private static async Task AddProducts(StoreContext context)
    {
        if (!context.Products.Any())
        {
            string data = File.ReadAllText("..\\..\\SeedData\\products.json");
            List<Product>? entities = JsonSerializer.Deserialize<List<Product>>(data);
            context.Products.AddRange(entities!);
            await context.SaveChangesAsync();
        }
    }
}
