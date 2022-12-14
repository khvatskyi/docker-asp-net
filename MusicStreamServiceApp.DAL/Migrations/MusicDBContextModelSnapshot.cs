// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicStreamServiceApp.DAL.EFCoreContexts;

namespace MusicStreamServiceApp.DAL.Migrations
{
    [DbContext(typeof(MusicDBContext))]
    partial class MusicDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "8e44755b-e6a5-4ea0-83a7-3cbd92422b47",
                            ConcurrencyStamp = "ed53eaa1-6dbc-416e-9b66-6a5814531057",
                            Name = "Visitor",
                            NormalizedName = "VISITOR"
                        },
                        new
                        {
                            Id = "eaeba048-16c7-4d46-bec6-e61d7c73d2ea",
                            ConcurrencyStamp = "f9ef561c-1762-40d2-8807-0b5fc13e1773",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Author")
                        .IsUnique()
                        .HasName("UIX_AlbumTable_Name_Author");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Бумбокс",
                            Name = "Голий король",
                            PhotoPath = "Images/NakedKing.jpg",
                            Year = 2017
                        },
                        new
                        {
                            Id = 2,
                            Author = "Сергій Бабкін",
                            Name = "Музасфера",
                            PhotoPath = "Images/Muzasfera.jpg",
                            Year = 2018
                        },
                        new
                        {
                            Id = 3,
                            Author = "Один в каное",
                            Name = "Один в каное",
                            PhotoPath = "Images/OdinVKanoe.jpg",
                            Year = 2016
                        },
                        new
                        {
                            Id = 4,
                            Author = "Бумбокс",
                            Name = "III",
                            PhotoPath = "Images/III.jpg",
                            Year = 2008
                        });
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UIX_GenreName");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Поп-музика"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Рок"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Фанк"
                        });
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("Name", "Author", "Year")
                        .IsUnique()
                        .HasName("UIX_Music_Name_Author_Year")
                        .HasFilter("[Year] IS NOT NULL");

                    b.ToTable("Music");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlbumId = 2,
                            Author = "Сергій Бабкін",
                            Name = "Де би я",
                            Year = 2018
                        },
                        new
                        {
                            Id = 2,
                            Author = "Бумбокс",
                            Name = "Квіти у волоссі",
                            Year = 2006
                        },
                        new
                        {
                            Id = 3,
                            Author = "Letay",
                            Name = "Ми вільні",
                            Year = 2020
                        },
                        new
                        {
                            Id = 4,
                            AlbumId = 4,
                            Author = "Бумбокс",
                            Name = "Наодинці",
                            Year = 2006
                        },
                        new
                        {
                            Id = 5,
                            Author = "Мотор'Ролла",
                            Name = "8-Ий колір",
                            Year = 2005
                        },
                        new
                        {
                            Id = 6,
                            AlbumId = 3,
                            Author = "Один в каное",
                            Name = "Човен",
                            Year = 2016
                        },
                        new
                        {
                            Id = 7,
                            AlbumId = 1,
                            Author = "Бумбокс",
                            Name = "Сталеві квіти",
                            Year = 2017
                        },
                        new
                        {
                            Id = 8,
                            Author = "Letay",
                            Name = "Мила моя",
                            Year = 2018
                        },
                        new
                        {
                            Id = 9,
                            AlbumId = 3,
                            Author = "Один в каное",
                            Name = "Пообіцяй мені",
                            Year = 2016
                        },
                        new
                        {
                            Id = 10,
                            Author = "Сергій Бабкін",
                            Name = "Дихай повільно",
                            Year = 2018
                        });
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.MusicGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("MusicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MusicId", "GenreId")
                        .IsUnique()
                        .HasName("UIX_MusicGenreTable_MusicId_GenreId");

                    b.ToTable("MusicGenres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 1,
                            MusicId = 1
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 1,
                            MusicId = 2
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 1,
                            MusicId = 3
                        },
                        new
                        {
                            Id = 4,
                            GenreId = 1,
                            MusicId = 4
                        },
                        new
                        {
                            Id = 5,
                            GenreId = 1,
                            MusicId = 5
                        },
                        new
                        {
                            Id = 6,
                            GenreId = 1,
                            MusicId = 6
                        },
                        new
                        {
                            Id = 7,
                            GenreId = 1,
                            MusicId = 7
                        },
                        new
                        {
                            Id = 8,
                            GenreId = 1,
                            MusicId = 8
                        },
                        new
                        {
                            Id = 9,
                            GenreId = 1,
                            MusicId = 9
                        },
                        new
                        {
                            Id = 10,
                            GenreId = 1,
                            MusicId = 10
                        });
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.MusicPlaylist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MusicId")
                        .HasColumnType("int");

                    b.Property<int>("UserPlaylistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MusicId");

                    b.HasIndex("UserPlaylistId", "MusicId")
                        .IsUnique()
                        .HasName("UIX_PlaylistMusic");

                    b.ToTable("MusicPlaylists");
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "f9c849ab-ee62-4561-a1ee-d244e001aa3d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "651335c2-b7a6-4149-871a-95e1f61f10b6",
                            Email = "test@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Oleksandr",
                            LastName = "Slobodian",
                            LockoutEnabled = false,
                            NormalizedEmail = "TEST@GMAIL.COM",
                            NormalizedUserName = "CARENDOH",
                            PasswordHash = "AQAAAAEAACcQAAAAEEYOfbhhZP2uqLre7aNmNHV2NkGpy2nUQwx6Y8khu2hOAj2VZ1Q6hNyu34Yq0Dcl4g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "dc82c475-2176-48f1-87dc-de39a380333b",
                            TwoFactorEnabled = false,
                            UserName = "Carendoh"
                        });
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.UserPlaylist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "PlaylistName")
                        .IsUnique()
                        .HasName("UIX_UserPlaylists")
                        .HasFilter("[UserId] IS NOT NULL AND [PlaylistName] IS NOT NULL");

                    b.ToTable("UserPlaylists");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicStreamServiceApp.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.Music", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.Album", "Album")
                        .WithMany("Musics")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.MusicGenre", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.Genre", "Genre")
                        .WithMany("MusicGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicStreamServiceApp.DAL.Entities.Music", "Music")
                        .WithMany("MusicGenres")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.MusicPlaylist", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.Music", "Music")
                        .WithMany("MusicPlaylists")
                        .HasForeignKey("MusicId")
                        .HasConstraintName("FK_MusicPlaylists_Music")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicStreamServiceApp.DAL.Entities.UserPlaylist", "UserPlaylist")
                        .WithMany("MusicPlaylists")
                        .HasForeignKey("UserPlaylistId")
                        .HasConstraintName("FK_MusicPlaylists_UserPlaylists")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicStreamServiceApp.DAL.Entities.UserPlaylist", b =>
                {
                    b.HasOne("MusicStreamServiceApp.DAL.Entities.User", "User")
                        .WithMany("UserPlaylists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserPlaylists_Users")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
