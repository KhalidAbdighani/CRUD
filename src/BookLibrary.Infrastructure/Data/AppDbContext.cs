using Microsoft.EntityFrameworkCore;

using BookLibrary.Domain.Entities;
namespace BookLibrary.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
   
     
    public DbSet<BookColums> BookStore { get; set; }
    public DbSet<Category> Categories {get; set;}
}