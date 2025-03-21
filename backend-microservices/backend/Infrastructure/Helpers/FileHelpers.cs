// Copyright : Pierre FRAISSE
// backend>backend>FileHelpers.cs
// Created : 2024/05/1414 - 13:05

using System.Diagnostics;
using System.Text.RegularExpressions;
using Core.Interfaces.IHelpers;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helpers;

public class FileHelpers : IFileHelpers
{
    public string GenerateFileName(string originalFileName)
    {
        var guid = Guid.NewGuid().ToString();
        var sanitizedFileName = Path.GetFileNameWithoutExtension(originalFileName);
        var extension = Path.GetExtension(originalFileName);
        return $"{guid}_{sanitizedFileName}{extension}";
    }

    public string GetFullFilePath(string fileName, string folderPath)
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        return Path.Combine(folderPath, fileName);
    }

    public async Task<string> SaveFileAsync(IFormFile file, string folderPath)
    {
        var fileName = GenerateFileName(file.FileName);
        var fullPath = GetFullFilePath(fileName, folderPath);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fullPath;
    }

    public async Task<string> UpdateFileAsync(
        IFormFile file,
        string existingFilePath,
        string folderPath
    )
    {
        if (File.Exists(existingFilePath))
            File.Delete(existingFilePath);
        return await SaveFileAsync(file, folderPath);
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public async Task<string> GetAudioFileLengthAsync(IFormFile audioFile)
    {
        string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        try
        {
            using (var tempStream = new FileStream(tempFilePath, FileMode.Create))
            {
                await audioFile.CopyToAsync(tempStream);
            }

            string length = await ExtractAudioLengthAsync(tempFilePath);
            File.Delete(tempFilePath);

            if (length == "00:00")
                throw new InvalidOperationException("Audio file cannot have a length of 0s.");

            return length;
        }
        finally
        {
            DeleteFile(tempFilePath);
        }
    }

    public async Task<string> ExtractAudioLengthAsync(string filePath)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{filePath}\"",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            },
        };
        process.Start();

        string stderr = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();

        var match = Regex.Match(stderr, @"Duration: \d{2}:(\d{2}:\d{2})\.\d{2}");
        if (match.Success)
            return match.Groups[1].Value;
        throw new InvalidOperationException("Unable to determine the length of the audio file.");
    }
}
