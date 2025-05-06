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
    Task<bool> DeleteWithFilesAsync(Guid id);

    async Task<bool> IGenericService<TEntity, TDto>.DeleteAsync(Guid id)
    {
        return await DeleteWithFilesAsync(id);
    }
}
