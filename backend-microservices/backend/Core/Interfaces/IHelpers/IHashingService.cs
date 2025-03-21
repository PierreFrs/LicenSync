// Copyright : Pierre FRAISSE
// backend>backend>IHashingService.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backend => IHashingService.cs
// Created : 2024/01/11 - 09:35
// Updated : 2024/01/11 - 09:35

namespace Core.Interfaces.IHelpers;

public interface IHashingService
{
    Task<string?> HashAudioFileAsync(string audioFilePath);
}
