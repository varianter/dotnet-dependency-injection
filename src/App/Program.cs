using App;
using Database;

var builder = ScreenHostBuilder.CreateDefaultBuilder();

builder.AddScreens();

builder.Services.AddSingleton<IDb, Db>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();

var app = builder.Build();

app.Run();