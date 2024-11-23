using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class MainDbContext: DbContext
{
    public MainDbContext(DbContextOptions
        <MainDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<User> Users { get; set; }
    
    //TODO: figure out what this function does
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<User>(entity => {
    //         entity.HasKey(k => k.Id);
    //     });
    //     OnModelCreatingPartial(modelBuilder);
    // }
    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}