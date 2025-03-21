// <copyright file="GenericService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Infrastructure.Services;

public class GenericService<TEntity, TDto>(
    IGenericRepository<TEntity> repository,
    IMapper mapper
) : IGenericService<TEntity, TDto>
    where TDto : BaseDto
    where TEntity : Tracker
{
    public async Task<TDto?> CreateAsync(TDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        var createdEntity = await repository.CreateAsync(entity);
        return mapper.Map<TDto>(createdEntity) ?? null;
    }

    public async Task<IReadOnlyList<TDto>> GetListAsync()
    {
        var entities = await repository.GetListAsync();
        return mapper.Map<List<TDto>>(entities);
    }

    public async Task<IReadOnlyList<TResult>> GetCardListByUserIdAsync<TResult>(
        ISpecification<TEntity, TResult> specs
    )
        where TResult : BaseDto
    {
        return await repository.GetCardListByUserIdAsync(specs);
    }

    public async Task<TDto?> GetByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        return mapper.Map<TDto>(entity) ?? null;
    }

    public async Task<TDto?> UpdateAsync(Guid id, TDto dto)
    {
        var existingEntity = await repository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"No entity found with ID {id}", nameof(id));
        }

        mapper.Map(dto, existingEntity);
        existingEntity.UpdateDate = DateTime.Now;

        var updatedEntity = await repository.UpdateAsync(existingEntity);

        return mapper.Map<TDto>(updatedEntity) ?? null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new ArgumentException($"No entity found with ID {id}", nameof(id));
        }

        return await repository.DeleteAsync(id);
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        return await repository.CountAsync(spec);
    }
}
