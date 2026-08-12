using Microsoft.EntityFrameworkCore;

using BookLibrary.Domain.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
   
     
    public DbSet<BookColums> BookStore { get; set; }
}