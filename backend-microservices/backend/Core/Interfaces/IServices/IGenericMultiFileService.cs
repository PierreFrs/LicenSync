// <copyright file="IGenericMultiFileService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.IServices;

public interface IGenericMultiFileService<TEntity, TDto>
    : IGenericService<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    Task<TDto?> CreateWithFilesAsync(TDto dto, IFormFile audioFile, IFormFile? imageFile);

    Task<TDto?> UpdateWithFilesAsync(Guid id, TDto dto, IFormFile? visualFile);

    Task<bool> DeleteWithFilesAsync(Guid id);

    async Task<TDto?> IGenericService<TEntity, TDto>.CreateAsync(TDto dto)
    {
        return await CreateWithFilesAsync(dto, default!, null);
    }

    async Task<TDto?> IGenericService<TEntity, TDto>.UpdateAsync(
        Guid id,
        TDto dto
    )
    {
        return await UpdateWithFilesAsync(id, dto, null);
    }

    async Task<bool> IGenericService<TEntity, TDto>.DeleteAsync(Guid id)
    {
        return await DeleteWithFilesAsync(id);
    }
}
