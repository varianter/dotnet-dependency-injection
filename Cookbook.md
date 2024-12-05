# Build Your Own Dependency Injection Cookbook

## Introduction

In this cookbook we'll be building a simple Dependency Injection (DI) container from scratch. We'll start simple and you can take it as far as you want.

The topics are:

1. [Registering services (Beginner)](#registering-services-beginner)
2. [Resolving services (Beginner)](#resolving-services-beginner)
3. [Handling lifetimes of services (Intermediate)](#handling-lifetimes-of-services-intermediate)
4. [Supporting nested dependencies (Intermediate)](#supporting-nested-dependencies-intermediate)
5. [Refactor App-project to use our DI-container (Intermediate)](#refactor-app-project-to-use-our-di-container-intermediate)
6. [Dispose of services when scope ends (Advanced)](#dispose-of-services-when-scope-ends-advanced)
7. [Handling circular dependencies (Advanced)](#handling-circular-dependencies-advanced)
8. [Automating the registration of services (Advanced)](#automating-the-registration-of-services-advanced)

We'll be building the DI-container in a test-driven manner, and the tests are already set up for you in the `DependencyInjection.Tests`-project.

In the fifth topic we'll refactor the `App`-project to use our DI-container, which will not use the unit tests, but rather manual testing of the app.

## Registering services (Beginner)

In this section we'll implement the `Add`-method in the [`ServiceCollection`](./src/DependencyInjection/ServiceCollection.cs)-class. This method should be able to register services with the DI-container.

The `ServiceCollection`-class is in essence a container for `ServiceDescriptor`-objects. A `ServiceDescriptor`-object contains information about a service, such as the service type, the implementation type and the lifetime of the service.

This type is used when configuring the application, before the application starts.

Work with the tests in the [`AddServiceTests`](./tests/DependencyInjection.Tests/AddServiceTests.cs)-class to check if the implementation is correct.

## Resolving services (Beginner)

In this section we'll implement the `GetService`- and `GetServices`-methods in the [`ServiceProvider`](./src/DependencyInjection/ServiceProvider.cs)-class. These methods should be able to resolve services from the DI-container. In addition we will have to implement the `BuildServiceProvider`-method in the [`ServiceCollection`](./src/DependencyInjection/ServiceCollection.cs)-class in order to get our `ServiceProvider`.

The `ServiceProvider`-class is typically used after the application has started to resolve services. You normally do not use this directly, but it is used by the application host.

Work with the tests in the [`GetServiceTests`](./tests/DependencyInjection.Tests/GetServiceTests.cs)-class to check if the implementation is correct.

### Hints

- You can use the `Activator.CreateInstance`-method to create an instance of a type.
- Unsure of the difference between `Transient`, `Scoped` and `Singleton` services? It doesn't matter for this section. We'll cover that in the next section. For now, you can just return a new instance every time.

## Handling lifetimes of services (Intermediate)

In this section we'll handle the lifetimes of services when resolving them with the `ServiceProvider`.

The lifetimes of services are typically one of three types:

- Transient: A new instance is created every time the service is resolved.
- Scoped: A new instance is created once per scope. In a web application, a scope is typically a single HTTP request.
- Singleton: A single instance is created and shared throughout the application.

This means we will have to implement the `CreateScope`-method in the [`ServiceProvider`](./src/DependencyInjection/ServiceProvider.cs)-class, and create a new `ServiceProvider` in the [`ServiceScope`](./src/DependencyInjection/ServiceScope.cs)-class (ignore the `Dispose`-method for now).

Also the resolving of services in the `ServiceProvider`-class should be handled differently depending on the lifetime of the service. Work with the tests in the [`LifetimeTests`](./tests/DependencyInjection.Tests/LifetimeTests.cs)-class to check if the implementation is correct.

### Hints

- Send in the `ServiceDescriptor`-list to the `ServiceScope`-class, so it can construct a new `ServiceProvider` with the correct services definitions.
- A `Singleton` service should only be created once, and the same instance should be returned every time it is resolved. Even across `ServiceScope`s! Maybe you can extend the `ServiceDescriptor`-class with a property to store the instance?
- A `Scoped` service should be created once per `ServiceScope`, and not shared between different `ServiceScope`s. Should it be cached somehow in each `ServiceProvider`?

## Supporting nested dependencies (Intermediate)

In this section we'll support nested dependencies when resolving services with the `ServiceProvider`. This means that a service can have dependencies on other services, which in turn can have dependencies on other services, and so on.

In order to do this, you'll have to find out which parameters a constructor of a service has, and resolve those services before creating the service.

In other words, you must recursively resolve dependencies when resolving a service. Work with the tests in the [`NestedDependenciesTests`](./tests/DependencyInjection.Tests/NestedDependenciesTests.cs)-class to check if the implementation is correct.

### Hints

- Use the `GetConstructors`-method on the `Type`-class to get the constructors of a type. To make it simple, you can assume that the first constructor is the one to use: `var constructor = descriptor.ImplementationType.GetConstructors().First();`
- Call `GetParameters` on the `ConstructorInfo`-class to get the parameters of the constructor: `var parameters = constructor.GetParameters();`. The `ParameterType`-property on the `ParameterInfo`-class will give you the of the nested dependency.
- Recursively calling `GetService`-method is probably a good idea here: `var nestedDependencies = constructor.GetParameters().Select(p => GetService(p.ParameterType));`
- The `Activator.CreateInstance`-method accepts an array of objects to use as parameters for the constructor: `Activator.CreateInstance(descriptor.ImplementationType, nestedDependencies.ToArray());`

## Refactor App-project to use our DI-container (Intermediate)

Now that we have a working DI-container, it's time to refactor the `App`-project to use it.

Add a `ServiceCollection`-property to the [`ScreenHostBuilder`](./src/App/ScreenHostBuilder.cs)-class, and use it to register the screens as services in the `AddScreens`-method: `Services.AddTransient<IScreen, AboutScreen>();`.

Also modify `Program.cs` in the `App`-project to use the `builder.Services` to register the `IDb` and `ITodoRepository` dependencies that the screens need.

Lastly, modify the `Build`-method in `ScreenHostBuilder` to use the give the `ScreenProvider`-class a `ServiceProvider` instead, and modify the `ScreenProvider`-class to dynamically resolve the screens from the `ServiceProvider`.

Run the application to see if it works as before - but now with the DI-container!

### Hints

- You have to add a dependency to the `DependencyInjection`-project from the `App`-project in order to use the `ServiceCollection` and `ServiceProvider` classes.
- You should not have to change the `ScreenHost`-class at all. The `ScreenProvider`-class should be the only class that uses the `ServiceProvider`.
- How you register the services in the `ServiceCollection` is up to you, but the lifetime you choose will change the behavior of the application. For example, if you register the `IDb` as a `Singleton`, the same database-instance will be used throughout the application. Play around with the lifetimes of the screens or their dependencies to see how it affects the application.

## Dispose of services when scope ends (Advanced)

In this section you will implement the `Dispose`-method in the [`ServiceScope`](./src/DependencyInjection/ServiceScope.cs)-class. This method should dispose of all services that are `IDisposable` when the scope ends.

Work with the tests in the [`ScopedDisposalTests`](./tests/DependencyInjection.Tests/ScopedDisposalTests.cs)-class to check if the implementation is correct.

This is an advanced topic, and you can skip it if you want. No hints!

## Handling circular dependencies (Advanced)

In this section you will handle circular dependencies when resolving services with the `ServiceProvider`. This means that a service can have a dependency on another service, which in turn has a dependency on the first service.

Work with the tests in the [`CircularDependencyTests`](./tests/DependencyInjection.Tests/CircularDependencyTests.cs)-class to check if the implementation is correct. The test expects that a `InvalidOperationException`-exception is thrown when a circular dependency is detected.

This is an advanced topic, and you can skip it if you want.

### Hints

- A simple way to detect circular dependencies is to keep track of the services that are currently being resolved. If a service is being resolved that is already in the list, you have a circular dependency. This might require quite a bit of refactoring of the `ServiceProvider`-class.
- A `HashSet<ServiceDescriptor>` might be a good data structure to use to keep track of the services that are currently being resolved.

## Automating the registration of services (Advanced)

A lot of libraries (like for example `Mediatr`) can do assembly scanning to automatically register services in the DI-container. We can do the same by modifying the `AddScreens`-method in the `ScreenHostBuilder`-class to scan the `App`-project for types that implement `IScreen` and register them as services.

This is a more advanced topic, and you can skip it if you want.

### Hints

- You can get the screen types from the current assembly with: `var screenTypes = typeof(IScreen).Assembly.GetTypes().Where(t => typeof(IScreen).IsAssignableFrom(t) && !t.IsInterface).ToList();`

## Conclusion

Congratulations! You have now built a simple Dependency Injection container from scratch. I'm not saying this is the correct or performant way to go about, but I'm hoping you've learnt that DI is not magic. It's just code.
