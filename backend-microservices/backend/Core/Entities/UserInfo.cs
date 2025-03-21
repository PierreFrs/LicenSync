// <copyright file="UserInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Core.Entities;

public class UserInfo : Tracker
{
    public string? PhoneNumber { get; set; }

    public DateTime? Birthdate { get; set; }

    public string? ProfilePicturePath { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }
}
