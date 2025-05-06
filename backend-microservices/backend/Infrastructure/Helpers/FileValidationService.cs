// Copyright : Pierre FRAISSE
// backend>backend>FileValidationService.cs
// Created : 2024/05/1414 - 13:05

using Core.Interfaces.IHelpers;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Helpers;

public class FileValidationService(IOptions<FileValidationSettings> fileValidationSettings)
    : IFileValidationService
{
    public (bool IsValid, string? ErrorMessage) ValidatePictureFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return (false, "File is empty or not provided");
        }

        List<string> allowedExtensions = fileValidationSettings.Value.AllowedPictureExtensions.Split(',')
            .Select(ext => ext.Trim().ToLower())
            .ToList();

        long pictureFileSizeLimit = fileValidationSettings.Value.PictureFileSizeLimit;
        double fileSizeLimitMb = Math.Round(pictureFileSizeLimit / (1024.0 * 1024.0), 2);

        if (file.Length > pictureFileSizeLimit)
        {
            return (false, $"File size exceeds the maximum limit of {fileSizeLimitMb}MB");
        }

        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        bool isExtensionAllowed = allowedExtensions.Contains(fileExtension);

        if (!isExtensionAllowed)
        {
            return (false, $"Invalid file extension. Only {String.Join(", ", allowedExtensions)} files are allowed");
        }

        return (true, null);
    }

    public (bool IsValid, string? ErrorMessage) ValidateAudioFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return (false, "File is empty or not provided");
        }

        List<string> allowedExtensions = fileValidationSettings.Value.AllowedAudioExtensions.Split(',')
            .Select(ext => ext.Trim().ToLower())
            .ToList();

        long audioFileSizeLimit = fileValidationSettings.Value.AudioFileSizeLimit;
        double fileSizeLimitMb = Math.Round(audioFileSizeLimit / (1024.0 * 1024.0), 2);

        if (file.Length > audioFileSizeLimit)
        {
            return (false, $"File size exceeds the maximum limit of {fileSizeLimitMb}MB");
        }

        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        bool isExtensionAllowed = allowedExtensions.Contains(fileExtension);

        if (!isExtensionAllowed)
        {
            return (false, $"Invalid file extension. Only {String.Join(", ", allowedExtensions)} files are allowed");
        }

        return (true, null);
    }
}
