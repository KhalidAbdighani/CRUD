using Lib_app;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
//imports >> dependences
var Book_DBB = builder.Configuration.GetConnectionString("Book_DB");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Book_DBB));
builder.Services.AddValidation();


var app = builder.Build();
//routs and uses 

app.MapEndpoints();

app.Run();
