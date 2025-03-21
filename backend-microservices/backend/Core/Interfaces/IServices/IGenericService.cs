// <copyright file="IGenericService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IGenericService<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    Task<TDto?> CreateAsync(TDto dto);

    Task<IReadOnlyList<TDto>> GetListAsync();

    Task<IReadOnlyList<TResult>> GetCardListByUserIdAsync<TResult>(
        ISpecification<TEntity, TResult> specs
    )
        where TResult : BaseDto;

    Task<TDto?> GetByIdAsync(Guid id);

    Task<TDto?> UpdateAsync(Guid id, TDto dto);

    Task<bool> DeleteAsync(Guid id);

    Task<int> CountAsync(ISpecification<TEntity> spec);
}
