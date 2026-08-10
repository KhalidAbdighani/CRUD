using Lib_app;
var builder = WebApplication.CreateBuilder(args);
//imports >> dependences


var app = builder.Build();
//routs and uses 

app.MapEndpoints();

app.Run();
