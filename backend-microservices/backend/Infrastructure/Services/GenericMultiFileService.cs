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

    public async Task<TDto?> CreateWithFilesAsync(
        TDto dto,
        IFormFile audioFile,
        IFormFile? imageFile
    )
    {
        // Handles audio file
        string length = await fileHelpers.GetAudioFileLengthAsync(audioFile);
        string audioFolder = GetAudioFolder();
        string fullAudioFilePath = await fileHelpers.SaveFileAsync(audioFile, audioFolder);

        // Handles visual file
        string? fullVisualFilePath = null;
        if (imageFile != null)
        {
            string visualFolder = GetVisualFolder();
            fullVisualFilePath = await fileHelpers.SaveFileAsync(imageFile, visualFolder);
        }

        // Creates entity
        var entity = _mapper.Map<TEntity>(dto);
        SetAudioFilePath(entity, fullAudioFilePath);
        if (fullVisualFilePath != null)
        {
            SetVisualFilePath(entity, fullVisualFilePath);
        }

        SetLength(entity, length);

        var createdEntity = await _repository.CreateAsync(entity);

        if (createdEntity == null)
        {
            return null;
        }

        return _mapper.Map<TDto>(createdEntity);
    }

    public async Task<TDto?> UpdateWithFilesAsync(
        Guid id,
        TDto dto,
        IFormFile? visualFile
    )
    {
        var existingEntity = await _repository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"No entity found with ID {id}");
        }

        if (visualFile != null)
        {
            string visualFolder = GetVisualFolder();
            string? oldVisualFilePath = GetVisualFilePath(existingEntity);
            if (oldVisualFilePath != null)
            {
                fileHelpers.DeleteFile(oldVisualFilePath);
            }

            string newVisualFilePath = await fileHelpers.SaveFileAsync(visualFile, visualFolder);
            SetVisualFilePath(existingEntity, newVisualFilePath);
        }

        _mapper.Map(dto, existingEntity);
        existingEntity.UpdateDate = DateTime.Now;

        var updatedEntity = await _repository.UpdateAsync(existingEntity);

        if (updatedEntity == null)
        {
            return null;
        }

        return _mapper.Map<TDto>(updatedEntity);
    }

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

        string? visualFilePath = GetVisualFilePath(existingEntity);
        if (visualFilePath != null)
        {
            fileHelpers.DeleteFile(visualFilePath);
        }

        return await _repository.DeleteAsync(id);
    }

    protected abstract string? GetAudioFilePath(TEntity entity);

    protected abstract string? GetVisualFilePath(TEntity entity);

    protected abstract string GetAudioFolder();

    protected abstract string GetVisualFolder();

    protected abstract void SetAudioFilePath(TEntity entity, string filePath);

    protected abstract void SetVisualFilePath(TEntity entity, string filePath);

    protected abstract void SetLength(TEntity entity, string length);
}
