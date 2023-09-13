using MeretTune.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeretTune.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>,Guid>
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Song { get; set; }
    //public DbSet<Playlist> Playlists { get; set; }
    public DbSet<ArtistFollowers> ArtistFollowers { get; set; }
    public DbSet<AlbumUsers> AlbumUsers { get; set; }
    //public DbSet<UserInfo> UserInfos { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ArtistFollowers>().HasKey(af => new { af.ApplicationUserId, af.ArtistId });
        builder.Entity<ArtistFollowers>()
            .HasOne(af => af.Followers)
            .WithMany(a => a.FollowedArtists)
            .HasForeignKey(af => af.ApplicationUserId);

        builder.Entity<ArtistFollowers>()
            .HasOne(af => af.Artist)
            .WithMany(a => a.Followers)
            .HasForeignKey(af => af.ArtistId);
        
        builder.Entity<AlbumUsers>().HasKey(af => new { af.ApplicationUserId, af.AlbumId });
                builder.Entity<AlbumUsers>()
                    .HasOne(af => af.Buyers)
                    .WithMany(a => a.BoughtAlbums)
                    .HasForeignKey(af => af.ApplicationUserId);
        
                builder.Entity<AlbumUsers>()
                    .HasOne(af => af.Album)
                    .WithMany(a => a.Buyers)
                    .HasForeignKey(af => af.AlbumId);
        
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after callinguse base.OnModelCreating(builder);
    }
}
