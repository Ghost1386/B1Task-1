using B1Task_1.Models;
using Microsoft.EntityFrameworkCore;

namespace B1Task_1;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=B1-1;" +
                                    "Trusted_Connection=True;TrustServerCertificate=True");
    }
    
    public DbSet<Data> Data { get; set; }
}