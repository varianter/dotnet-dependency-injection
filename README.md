# Build your own Dependency Injection Container in C# and Dotnet

## What is this codebase about?

A lot of developers struggle with understanding how Dependency Injection (DI) works, because its inner workings are often hidden away. It can feel like magic ✨, and that confusion often leads to misusage and bugs.

This codebase is a starter template for building a simple DI-container from scratch, with the intention of demystifying how they work.

See the [Cookbook](./Cookbook.md) for a step-by-step guide on how to build your own DI-container.

## Wait, what is Dependency Injection?

Dependency Injection is a technique that you will come across in many C# and Dotnet-projects.

What we want to achieve with DI is "Inversion of Control" (IoC), which is a design pattern that in simple terms can be described as; "instead of creating the dependencies yourself, you should have them created for you".

In essence this makes your code and their dependencies more loosely coupled, and as such more manageable and not to mention easier to test. If you don't construct your dependencies yourself, it is easier to swap them out, or mock them in your tests.

This issues of not using DI normally doesn't crop up in small projects, but as your solution grows you will notice that the tightly coupling between code and their dependencies makes new changes and maintenance difficult.

### Code example

In essence it is the difference between doing this:

```csharp
public class WeatherUI
{
    // Explicitly creating the dependency
    private readonly IWeatherService _service = new WeatherService();
}
```

And this:

```csharp

public class WeatherUI
{
    private readonly IWeatherService _service;

    // Dependency is injected from the outside with interface,
    // not the concrete implementation
    public WeatherUI(IWeatherService service)
    {
        _service = service;
    }
}
```

## What is in this starter template?

It is split up into four projects:

- `src/App` - The Todo-application that needs to be refactored to use DI
- `src/Database` - A simple in-memory database that the application uses
- `src/DependencyInjection` - A skeleton project where we will build our DI implementation
- `tests/DependencyInjection.Tests` - Unit tests to help us build our DI-container

The `App`-project is a simple Todo-application build in a console application. It is meant to model a typical ASP.NET application, but with the concept of `Screens` instead of `Controllers`.

The `Program.cs` in the `App`-project is where the application starts, and is modeled after a typical ASP.NET entry point:

```csharp
var builder = ScreenHostBuilder.CreateDefaultBuilder();

builder.AddScreens();

var app = builder.Build();

app.Run();
```

This is where we will refactor the application to use our own DI-container.

In addition you have `ScreenHost` and `ScreenHostBuilder` in the `App`-project, which is a simple implementation of a host for our screens. These are meant to model the `WebApplication` and `WebApplicationBuilder` in ASP.NET.

But, the main focus is the `DependencyInjection`-project, where we will build our own DI-container. The `ServiceCollection`-class is where we will register our services, and the `ServiceProvider`-class is where we will resolve them. It is recommended you start with the tests in the `DependencyInjection.Tests`-project to guide you through the implementation, before refactoring the `App`-project.

Again, see the [Cookbook](./Cookbook.md) for a step-by-step guide on how to go about this.
