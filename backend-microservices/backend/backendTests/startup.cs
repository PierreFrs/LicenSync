// Copyright : Pierre FRAISSE
// backend>backendTests>startup.cs
// Created : 2024/05/1414 - 13:05

// Created by : Pierre FRAISSE
// backend => backendTests => startup.cs
// Created : 2024/01/08 - 10:25
// Updated : 2024/01/08 - 10:25

using backendTests.TestServices.Implementation;
using backendTests.TestServices.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace backendTests;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITestApplicationDbContext, TestApplicationDbContext>();
    }
}
