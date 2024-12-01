﻿namespace DependencyInjection;

public interface IServiceCollection
{
    public IServiceCollection AddTransient(Type serviceType, Type implementationType);

    public IServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService;

    public IServiceCollection AddScoped(Type serviceType, Type implementationType);

    public IServiceCollection AddScoped<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService;

    public IServiceCollection AddSingleton(Type serviceType, Type implementationType);

    public IServiceCollection AddSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService;

    public IServiceProvider BuildServiceProvider();
}