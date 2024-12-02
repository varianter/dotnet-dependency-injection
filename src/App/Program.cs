using System.Reflection;
using App;
using App.Screens;
using Database;

var builder = ScreenHostBuilder.CreateDefaultBuilder();

builder.AddScreen<AboutScreen>();
builder.AddScreen<TodoScreen>();

builder.Services.AddTransient<ITodoRepository, TodoRepository>();

var app = builder.Build();

app.Run();