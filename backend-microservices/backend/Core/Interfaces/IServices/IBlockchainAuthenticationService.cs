// Copyright : Pierre FRAISSE
// backend>backend>IBlockchainAuthenticationService.cs
// Created : 2024/05/1414 - 13:05

using Core.DTOs.TrackDTOs;
using Nethereum.RPC.Eth.DTOs;

namespace Core.Interfaces.IServices;

public interface IBlockchainAuthenticationService<TTrackDto>
    where TTrackDto : TrackDto
{
    Task<string?> DeployContractAsync();

    Task<TrackWithReceiptDto<TTrackDto>?> AuthenticateTrackAsync(Guid trackId);

    Task<TransactionReceipt?> StoreHashOnBlockchainAsync(
        string? contractAddress,
        Guid hashId,
        string trackHash
    );

    Task<string?> GenerateNewTrackHashAsync(Guid trackId);

    Task<string?> GetTrackHashFromBlockchainAsync(Guid trackId);
}
