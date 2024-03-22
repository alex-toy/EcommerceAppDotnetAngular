using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
{
    private readonly StoreContext _context;

    public GenericRepo(StoreContext context)
    {
        _context = context;
    }

    public Task<T> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }    
}
