// Copyright : Pierre FRAISSE
// backend>backend>IFileHelpers.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backend => IFileHelpers.cs
// Created : 2024/01/08 - 16:06
// Updated : 2024/01/08 - 16:06

using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.IHelpers;

public interface IFileHelpers
{
    string GenerateFileName(string originalFileName);

    string GetFullFilePath(string fileName, string folderPath);

    Task<string> SaveFileAsync(IFormFile file, string folderPath);

    Task<string> UpdateFileAsync(IFormFile file, string existingFilePath, string folderPath);

    void DeleteFile(string filePath);

    Task<string> GetAudioFileLengthAsync(IFormFile audioFile);

    Task<string> ExtractAudioLengthAsync(string filePath);
}
