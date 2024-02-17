using Microsoft.EntityFrameworkCore;

namespace UserService;

public class MyDbContext:DbContext
{
    public virtual DbSet<UserService.Entities.Profession> Professions { get; set; }
    public virtual DbSet<UserService.Entities.User> Users { get; set; }
    public virtual DbSet<UserService.Entities.LoginPassword> LoginPasswords { get; set; }

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