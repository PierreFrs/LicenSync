// Copyright : Pierre FRAISSE
// backend>backend>IFileValidationService.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backend => IFileValidationService.cs
// Created : 2023/12/13 - 11:56
// Updated : 2023/12/13 - 11:56

using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.IHelpers;

public interface IFileValidationService
{
    (bool IsValid, string? ErrorMessage) ValidatePictureFile(IFormFile file);

    (bool IsValid, string? ErrorMessage) ValidateAudioFile(IFormFile file);
}
