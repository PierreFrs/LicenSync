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
    public void ValidatePictureFile(IFormFile file)
    {
        string allowedExtensionsConfig = fileValidationSettings.Value.AllowedPictureExtensions;
        List<string> allowedExtensions = new List<string>(allowedExtensionsConfig.Split(','));

        long pictureFileSizeLimit = fileValidationSettings.Value.PictureFileSizeLimit;
        double fileSizeLimitMb = pictureFileSizeLimit / (1024.0 * 1024.0);

        if (file.Length > pictureFileSizeLimit)
        {
            throw new ArgumentException(
                $"File size exceeds the maximum limit of {fileSizeLimitMb}MB"
            );
        }

        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        bool isExtensionAllowed = allowedExtensions.Exists(ext =>
            fileExtension == ext.Trim().ToLower()
        );

        if (!isExtensionAllowed)
        {
            throw new ArgumentException(
                $"Invalid file extension. Only {String.Join(", ", allowedExtensions)} files are allowed"
            );
        }
    }

    public void ValidateAudioFile(IFormFile file)
    {
        string allowedExtensionsConfig = fileValidationSettings.Value.AllowedAudioExtensions;
        List<string> allowedExtensions = new List<string>(allowedExtensionsConfig.Split(','));

        long audioFileSizeLimit = fileValidationSettings.Value.AudioFileSizeLimit;
        double fileSizeLimitMb = audioFileSizeLimit / (1024.0 * 1024.0);

        if (file.Length > audioFileSizeLimit)
        {
            throw new ArgumentException(
                $"File size exceeds the maximum limit of {fileSizeLimitMb}MB"
            );
        }

        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        bool isExtensionAllowed = allowedExtensions.Exists(ext =>
            fileExtension == ext.Trim().ToLower()
        );

        if (!isExtensionAllowed)
        {
            throw new ArgumentException(
                $"Invalid file extension. Only {String.Join(", ", allowedExtensions)} files are allowed"
            );
        }
    }
}
