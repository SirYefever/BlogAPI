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
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Community> Communities { get; set; }
    public virtual DbSet<UserCommunity> UserCommunity { get; set; }
    
    //TODO: figure out what this function does
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<User>(entity => {
        //     entity.HasKey(k => k.Id);
        // });
        // OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<UserCommunity>()
            .HasKey(uc => new { uc.UserId, uc.CommunityId });        
    }
    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}