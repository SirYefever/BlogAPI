using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class MainDbContext : DbContext
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
    public virtual DbSet<PostTag> PostTag { get; set; }
    public virtual DbSet<CommunityPost> CommunityPost { get; set; }
    public virtual DbSet<Comment> Comment { get; set; }
    public virtual DbSet<PostLike> PostLike { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCommunity>()
            .HasKey(uc => new { uc.UserId, uc.CommunityId });

        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

        modelBuilder.Entity<CommunityPost>()
            .HasKey(cp => new { cp.CommunityId, cp.PostId });

        modelBuilder.Entity<PostLike>()
            .HasKey(pt => new { pt.PostId, pt.UserWhoLikedId });
    }
    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}