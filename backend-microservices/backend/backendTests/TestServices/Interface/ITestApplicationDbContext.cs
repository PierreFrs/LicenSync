// Copyright : Pierre FRAISSE
// backend>backendTests>ITestApplicationDbContext.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backendTests => ITestDataContext.cs
// Created : 2024/01/08 - 10:29
// Updated : 2024/01/08 - 10:29

using Infrastructure.Data;

namespace backendTests.TestServices.Interface;

public interface ITestApplicationDbContext
{
    public ApplicationDbContext GetContext();
}
