// Copyright : Pierre FRAISSE
// backend>backend>HashingService.cs
// Created : 2024/05/1616 - 11:05

using System.Security.Cryptography;
using Core.Interfaces.IHelpers;

namespace Infrastructure.Helpers;

public class HashingService : IHashingService
{
    public async Task<string?> HashAudioFileAsync(string audioFilePath)
    {
        if (!File.Exists(audioFilePath))
        {
            throw new FileNotFoundException("Could not find file", audioFilePath);
        }

        using (var ms = new MemoryStream())
        {
            using (var fileStream = new FileStream(audioFilePath, FileMode.Open, FileAccess.Read))
            {
                await fileStream.CopyToAsync(ms);
            }

            var fileBytes = ms.ToArray();

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(fileBytes);
            var hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();

            return hashString;
        }
    }
}
