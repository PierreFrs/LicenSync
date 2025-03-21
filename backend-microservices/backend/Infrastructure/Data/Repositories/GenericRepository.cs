// <copyright file="GenericRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    public async Task<T?> CreateAsync(T entity)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetListAsync()
    {
        return await context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await context.Set<T>().AsNoTracking().FirstAsync(x => x.Id == id);

            return entity;
        }
        catch (InvalidOperationException)
        {
            throw new KeyNotFoundException($"No entity with the given id: {id}");
        }
    }

    public async Task<IReadOnlyList<T>> GetEntityListBySpecificationAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).AsNoTracking().ToListAsync();
    }

    public async Task<TResult> GetCardBySpecificationAsync<TResult>(ISpecification<T, TResult> spec)
    where TResult : BaseDto
{
    return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync() 
        ?? throw new KeyNotFoundException($"No {typeof(T).Name} found matching the specification");
}

    public async Task<IReadOnlyList<TResult>> GetCardListByUserIdAsync<TResult>(
        ISpecification<T, TResult> spec
    )
        where TResult : BaseDto
    {
        return await ApplySpecification(spec).AsNoTracking().ToListAsync();
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        var existingEntity = await context.Set<T>().FindAsync(entity.Id);

        if (existingEntity == null)
        {
            throw new ArgumentException($"No entity with the given id: {entity.Id}");
        }

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingEntity = await context.Set<T>().FindAsync(id);

        if (existingEntity == null)
        {
            throw new ArgumentException($"No entity with the given id: {id}");
        }

        context.Set<T>().Remove(existingEntity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = context.Set<T>().AsQueryable();

        query = spec.ApplyCriteria(query);

        return await query.CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery<TResult>(context.Set<T>().AsQueryable(), spec);
    }
}
