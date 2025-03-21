// <copyright file="IGenericRepository.cs" company="p.fraisse@recygroup.local Pierre FRAISSE">
// Copyright (c) p.fraisse@recygroup.local Pierre FRAISSE. All rights reserved.
// </copyright>

using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IGenericRepository<T>
    where T : BaseEntity
{
    Task<T?> CreateAsync(T entity);

    Task<IReadOnlyList<T>> GetListAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<T>> GetEntityListBySpecificationAsync(ISpecification<T> spec);

    Task<IReadOnlyList<TResult>> GetCardListByUserIdAsync<TResult>(ISpecification<T, TResult> spec)
        where TResult : BaseDto;

    Task<TResult> GetCardBySpecificationAsync<TResult>(ISpecification<T, TResult> spec)
        where TResult : BaseDto;

    Task<T?> UpdateAsync(T entity);

    Task<bool> DeleteAsync(Guid id);

    Task<int> CountAsync(ISpecification<T> spec);
}
