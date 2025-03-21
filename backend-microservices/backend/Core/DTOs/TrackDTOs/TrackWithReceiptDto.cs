// Copyright : Pierre FRAISSE
// backend>backend>TrackWithReceiptDto.cs
// Created : 2024/05/1414 - 13:05

using Nethereum.RPC.Eth.DTOs;

namespace Core.DTOs.TrackDTOs;

public class TrackWithReceiptDto<TTrackDto>
    where TTrackDto : TrackDto
{
    public TTrackDto? TrackDto { get; set; }

    public TransactionReceipt? TransactionReceipt { get; set; }
}
