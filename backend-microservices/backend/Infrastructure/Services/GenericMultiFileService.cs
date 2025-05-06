// <copyright file="GenericMultiFileService.cs" company="PlaceholderCompany">
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

public abstract class GenericMultiFileService<TEntity, TDto>(
    IGenericRepository<TEntity> repository,
    IMapper mapper,
    IFileHelpers fileHelpers
)
    : GenericService<TEntity, TDto>(repository, mapper),
        IGenericMultiFileService<TEntity, TDto>
    where TEntity : Tracker
    where TDto : BaseDto
{
    private readonly IGenericRepository<TEntity> _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<bool> DeleteWithFilesAsync(Guid id)
    {
        var existingEntity = await _repository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"No entity found with ID {id}");
        }

        string? audioFilePath = GetAudioFilePath(existingEntity);
        if (audioFilePath != null)
        {
            fileHelpers.DeleteFile(audioFilePath);
        }

        return await _repository.DeleteAsync(id);
    }

    protected abstract string? GetAudioFilePath(TEntity entity);

    protected abstract string? GetVisualFilePath(TEntity entity);

    protected abstract string GetAudioFolder();

    protected abstract string GetVisualFolder();

    protected abstract void SetAudioFilePath(TEntity entity, string filePath);

    protected abstract void SetLength(TEntity entity, string length);
}
