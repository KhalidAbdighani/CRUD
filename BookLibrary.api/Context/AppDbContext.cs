using Microsoft.EntityFrameworkCore;

namespace Lib_app;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    
    public DbSet<BookColums> BookStore { get; set; }
}