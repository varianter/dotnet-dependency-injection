using App;
using App.Screens;
using Database;

var builder = ScreenHostBuilder.CreateDefaultBuilder();

// Hmm, this might become a bit unwieldy if we add more screens and their dependencies
// Is there some sort of pattern we could use to make this more manageable?:
var todoRepository = new TodoRepository();

builder
    .AddScreen(new AboutScreen())
    .AddScreen(new TodoScreen(todoRepository));

var app = builder.Build();

app.Run();