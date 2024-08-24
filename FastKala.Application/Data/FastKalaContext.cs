using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FastKala.Application.Data;

public class FastKalaContext : IdentityDbContext<FastKalaUser>
{
    public FastKalaContext(DbContextOptions<FastKalaContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
