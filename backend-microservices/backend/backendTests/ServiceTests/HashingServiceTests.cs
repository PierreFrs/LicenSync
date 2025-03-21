// Copyright : Pierre FRAISSE
// backend>backendTests>HashingServiceTests.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backendTests => HashingServiceTests.cs
// Created : 2024/02/08 - 11:12
// Updated : 2024/02/08 - 11:12

using System.Security.Cryptography;
using System.Text;
using Infrastructure.Helpers;

namespace backendTests.ServiceTests;

[Collection("ServiceTests")]
public class HashingServiceTests
{
    [Fact]
    public async Task HashAudioFileAsync_ReturnsHash_WithValidAudioFile()
    {
        // Arrange
        var hashingService = new HashingService();
        string testContent = "Test audio content";
        var expectedHash = CalculateSha256Hash(testContent);

        // Create a temporary file with known content
        var tempFilePath = Path.GetTempFileName();
        await File.WriteAllTextAsync(tempFilePath, testContent);

        try
        {
            // Act
            var resultHash = await hashingService.HashAudioFileAsync(tempFilePath);

            // Assert
            Assert.Equal(expectedHash, resultHash);
        }
        finally
        {
            // Cleanup
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }

    private static string CalculateSha256Hash(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256.ComputeHash(inputBytes);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}
