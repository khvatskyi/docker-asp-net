using Microsoft.EntityFrameworkCore;
using MusicStreamServiceApp.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace MusicStreamServiceApp.DAL.EFCoreContexts {
  public partial class MusicDBContext : IdentityDbContext<User>
    {
        public MusicDBContext(DbContextOptions<MusicDBContext> options)
            : base(options) {
            Database.EnsureCreated();
        }

        public virtual DbSet<UserPlaylist> UserPlaylists { get; set; }
        public virtual DbSet<MusicPlaylist> MusicPlaylists { get; set; }
        public virtual DbSet<Music> Music { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MusicGenre> MusicGenres { get; set; }
        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserPlaylist>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.PlaylistName })
                    .HasName("UIX_UserPlaylists")
                    .IsUnique();

                entity.Property(e => e.PlaylistName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPlaylists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserPlaylists_Users");
            });

            modelBuilder.Entity<MusicPlaylist>(entity =>
            {
                entity.HasIndex(e => new { e.UserPlaylistId, e.MusicId })
                    .HasName("UIX_PlaylistMusic")
                    .IsUnique();

                entity.HasOne(d => d.Music)
                    .WithMany(p => p.MusicPlaylists)
                    .HasForeignKey(d => d.MusicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MusicPlaylists_Music");

                entity.HasOne(d => d.UserPlaylist)
                    .WithMany(p => p.MusicPlaylists)
                    .HasForeignKey(d => d.UserPlaylistId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MusicPlaylists_UserPlaylists");
            });

            modelBuilder.Entity<Music>(entity =>
            {
                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => new { e.Name, e.Author, e.Year })
                    .IsUnique()
                    .HasName("UIX_Music_Name_Author_Year");

                entity.HasOne(m => m.Album)
                    .WithMany(a => a.Musics)
                    .HasForeignKey(m => m.AlbumId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique()
                    .HasName("UIX_GenreName");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<MusicGenre>(entity =>
            {
                entity.HasIndex(e => new { e.MusicId, e.GenreId })
                    .IsUnique()
                    .HasName("UIX_MusicGenreTable_MusicId_GenreId");

                entity.HasOne(mg => mg.Genre)
                    .WithMany(g => g.MusicGenres)
                    .HasForeignKey(mg => mg.GenreId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(mg => mg.Music)
                    .WithMany(m => m.MusicGenres)
                    .HasForeignKey(mg => mg.MusicId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Author })
                    .IsUnique()
                    .HasName("UIX_AlbumTable_Name_Author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Year)
                   .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
