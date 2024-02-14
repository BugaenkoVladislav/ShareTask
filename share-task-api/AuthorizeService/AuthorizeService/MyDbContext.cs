using AuthorizeService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorizeService;

public class MyDbContext:DbContext
{
    public virtual DbSet<Entities.Profession> Professions { get; set; }
    public virtual DbSet<Entities.User> Users { get; set; }
    public virtual DbSet<Entities.LoginPassword> LoginPasswords { get; set; }

    public MyDbContext()
    {
        
    }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}