﻿using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces;

public interface IGenericRepo<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<T> CreateAsync(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
}
