// <copyright file="LicenSyncDeploymentMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Nethereum.Contracts;

namespace Infrastructure.BlockchainMessage;

public class LicenSyncDeploymentMessage() : ContractDeploymentMessage(BYTECODE)
{
    public static string BYTECODE { get; } =
        Environment.GetEnvironmentVariable("LICENSYNC_BYTECODE") ?? string.Empty;
}
