// <copyright file="FileStorageSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Infrastructure.Settings;

public class FileStorageSettings
{
    public string? ProfilePicturesFolder { get; set; } = string.Empty;

    public string AlbumVisualsFolder { get; set; } = string.Empty;

    public string TrackVisualFilesFolder { get; set; } = string.Empty;

    public string TrackAudioFilesFolder { get; set; } = string.Empty;
}
