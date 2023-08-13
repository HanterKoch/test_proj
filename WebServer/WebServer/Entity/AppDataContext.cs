using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace WebServer.Entity
{
    public class AppDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Links> LinkData { get; set; } = null!;
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
