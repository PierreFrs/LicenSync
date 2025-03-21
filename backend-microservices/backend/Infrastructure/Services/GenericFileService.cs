// <copyright file="GenericFileService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public abstract class GenericFileService<TEntity, TDto>(
    IGenericRepository<TEntity> repository,
    IMapper mapper,
    IFileHelpers fileHelpers
)
    : GenericService<TEntity, TDto>(repository, mapper),
        IGenericFileService<TEntity, TDto>
    where TEntity : Tracker
    where TDto : BaseDto
{
    private readonly IGenericRepository<TEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<TDto?> CreateWithFileAsync(TDto dto, IFormFile? file)
    {
        if (file != null)
        {
            var folder = GetFolder();
            await fileHelpers.SaveFileAsync(file, folder);
        }

        var entity = _mapper.Map<TEntity>(dto);
        var createdEntity = await _repository.CreateAsync(entity);
        return _mapper.Map<TDto>(createdEntity);
    }

    public async Task<TDto?> UpdateWithFileAsync(Guid id, TDto dto, IFormFile? file)
    {
        var existingEntity = await _repository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            return null;
        }

        if (file != null)
        {
            var folder = GetFolder();
            var filePath = UpdateFile(existingEntity, file, folder);
            SetFilePath(existingEntity, filePath);
        }

        _mapper.Map(dto, existingEntity);
        SetUpdateDate(existingEntity);

        var updatedEntity = await _repository.UpdateAsync(existingEntity);
        return _mapper.Map<TDto>(updatedEntity);
    }

    public async Task<bool> DeleteWithFileAsync(Guid id)
    {
        var existingEntity = await _repository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"No entity found with ID {id}");
        }

        var filePath = GetFilePath(existingEntity);
        if (!string.IsNullOrEmpty(filePath))
        {
            var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            fileHelpers.DeleteFile(fullFilePath);
        }

        return await _repository.DeleteAsync(id);
    }

    protected abstract string GetFolder();

    protected abstract void SetFilePath(TEntity entity, string filePath);

    protected abstract void SetUpdateDate(TEntity entity);

    protected abstract string UpdateFile(TEntity entity, IFormFile file, string folder);

    protected abstract string GetFilePath(TEntity entity);
}
