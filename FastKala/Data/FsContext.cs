using FastKala.Models;
using Microsoft.EntityFrameworkCore;

namespace FastKala.Data;

public class FsContext : DbContext
{
    public FsContext()
    {
    }

    public FsContext(DbContextOptions<FsContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=FastKala;Trusted_Connection=yes;TrustServerCertificate=True;");
    }

    public DbSet<Product> Products { get; set; }
}