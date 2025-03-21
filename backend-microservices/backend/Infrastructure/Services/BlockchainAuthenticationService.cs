// <copyright file="BlockchainAuthenticationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Core.DTOs.TrackDTOs;
using Core.Entities;
using Core.Interfaces.IHelpers;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Infrastructure.BlockchainMessage;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Infrastructure.Services;

public class BlockchainAuthenticationService<TTrackDto>(
    Web3 web3,
    IHashingService hashingService,
    ITrackRepository trackRepository,
    IMapper mapper
) : IBlockchainAuthenticationService<TTrackDto>
    where TTrackDto : TrackDto
{
    /// <inheritdoc/>
    public async Task<string?> DeployContractAsync()
    {
        var account = new Account(Environment.GetEnvironmentVariable("BC_PRIVATE_KEY"));
        var web4 = new Web3(account, web3.Client);

        var deploymentMessage = new LicenSyncDeploymentMessage();

        var deploymentHandler = web4.Eth.GetContractDeploymentHandler<LicenSyncDeploymentMessage>();
        var transactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(
            deploymentMessage
        );

        return transactionReceipt.ContractAddress;
    }

    public async Task<TrackWithReceiptDto<TTrackDto>?> AuthenticateTrackAsync(Guid trackId)
    {
        string? trackHash = await GenerateNewTrackHashAsync(trackId);

        Guid hashId = Guid.NewGuid();

        if (trackHash != null)
        {
            TransactionReceipt? transactionReceipt = await StoreHashOnBlockchainAsync(
                Environment.GetEnvironmentVariable("LICENSYNC_CONTRACT_ADDRESS"),
                hashId,
                trackHash
            );

            var updatedTrack = await trackRepository.StoreHashGuidInDatabaseAsync(trackId, hashId);

            return new TrackWithReceiptDto<TTrackDto>
            {
                TrackDto = mapper.Map<TTrackDto>(updatedTrack),
                TransactionReceipt = transactionReceipt,
            };
        }

        return null;
    }

    public async Task<TransactionReceipt?> StoreHashOnBlockchainAsync(
        string? contractAddress,
        Guid hashId,
        string trackHash
    )
    {
        var account = new Account(Environment.GetEnvironmentVariable("BC_PRIVATE_KEY"));
        var web3WithAccount = new Web3(account, web3.Client);

        var contract = web3WithAccount.Eth.GetContract(
            Environment.GetEnvironmentVariable("LICENSYNC_ABI"),
            contractAddress
        );
        var storeTrackHashFunction = contract.GetFunction("storeTrackHash");

        byte[] hashIdByteArray = hashId.ToByteArray();
        byte[] bytes16 = new byte[16];
        Array.Copy(hashIdByteArray, bytes16, hashIdByteArray.Length);

        var transactionInput = storeTrackHashFunction.CreateTransactionInput(
            account.Address,
            new HexBigInteger(900000),
            null,
            bytes16,
            trackHash
        );
        var signedTransaction = await web3WithAccount.TransactionManager.SignTransactionAsync(
            transactionInput
        );
        var transactionHash =
            await web3WithAccount.Eth.Transactions.SendRawTransaction.SendRequestAsync(
                signedTransaction
            );
        var transactionReceipt =
            await web3WithAccount.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(
                transactionHash
            );

        while (transactionReceipt == null)
        {
            await Task.Delay(1000);
            transactionReceipt =
                await web3WithAccount.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(
                    transactionHash
                );
        }

        return transactionReceipt;
    }

    public async Task<string?> GenerateNewTrackHashAsync(Guid trackId)
    {
        Track? expectedTrack = await trackRepository.GetByIdAsync(trackId);

        if (expectedTrack == null || string.IsNullOrEmpty(expectedTrack.AudioFilePath))
        {
            throw new FileNotFoundException("The track does not exist");
        }

        string? trackHash = await hashingService.HashAudioFileAsync(expectedTrack.AudioFilePath);

        if (trackHash == null)
        {
            return null;
        }

        return trackHash;
    }

    public async Task<string?> GetTrackHashFromBlockchainAsync(Guid trackId)
    {
        var track = await trackRepository.GetByIdAsync(trackId);
        if (track == null || track.BlockchainHashId == null)
        {
            return null;
        }

        var hashId = track.BlockchainHashId.Value;
        byte[] hashIdByteArray = hashId.ToByteArray();
        byte[] bytes16 = new byte[16];
        Array.Copy(hashIdByteArray, bytes16, hashIdByteArray.Length);

        string trackHash = await GetHashFromBlockchainAsync(
            Environment.GetEnvironmentVariable("LICENSYNC_CONTRACT_ADDRESS"),
            bytes16
        );

        return trackHash;
    }

    private async Task<string> GetHashFromBlockchainAsync(string? contractAddress, byte[] hashId)
    {
        var contract = web3.Eth.GetContract(
            Environment.GetEnvironmentVariable("LICENSYNC_ABI"),
            contractAddress
        );
        var getTrackHashFunction = contract.GetFunction("getTrackHash");

        byte[] bytes16 = new byte[16];
        Array.Copy(hashId, bytes16, bytes16.Length);

        string trackHash = await getTrackHashFunction.CallAsync<string>(bytes16);
        return trackHash;
    }
}
