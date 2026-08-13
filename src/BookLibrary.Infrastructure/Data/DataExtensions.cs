using System;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
namespace BookLibrary.Infrastructure.Data;

public static class DataExtensions
{
    public static async Task CategoryDB (this IServiceProvider  services)
    {
        using (var scope = services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    
    if (!await db.Categories.AnyAsync())
    {
        db.Categories.AddRange(
            new Category { name = "Action" },
            new Category { name = "Drama" },
            new Category { name = "Programming" },
            new Category { name = "History" }
        );
        await db.SaveChangesAsync();
    }
}
    }
}
