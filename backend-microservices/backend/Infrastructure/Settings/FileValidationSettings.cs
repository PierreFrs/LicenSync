// <copyright file="FileValidationSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Infrastructure.Settings;

public class FileValidationSettings
{
    public string AllowedPictureExtensions { get; set; } = string.Empty;

    public string AllowedAudioExtensions { get; set; } = string.Empty;

    public long PictureFileSizeLimit { get; set; } = 0;

    public long AudioFileSizeLimit { get; set; } = 0;

    public string MimeType { get; set; } = string.Empty;
}
