// <copyright file="IGenericFileService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.IServices;

public interface IGenericFileService<TEntity, TDto>
    : IGenericService<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    Task<TDto?> CreateWithFileAsync(TDto dto, IFormFile? file);

    Task<TDto?> UpdateWithFileAsync(Guid id, TDto dto, IFormFile? file);

    Task<bool> DeleteWithFileAsync(Guid id);

    async Task<TDto?> IGenericService<TEntity, TDto>.CreateAsync(TDto dto)
    {
        return await CreateWithFileAsync(dto, null);
    }

    async Task<TDto?> IGenericService<TEntity, TDto>.UpdateAsync(
        Guid id,
        TDto dto
    )
    {
        return await UpdateWithFileAsync(id, dto, null);
    }

    async Task<bool> IGenericService<TEntity, TDto>.DeleteAsync(Guid id)
    {
        return await DeleteWithFileAsync(id);
    }
}
