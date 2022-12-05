using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicStreamServiceApp.DAL;
using MusicStreamServiceApp.DAL.EFCoreContexts;
using MusicStreamServiceApp.DAL.Interfaces;
using MusicStreamServiceApp.DAL.Interfaces.IEntityRepositories;
using MusicStreamServiceApp.DAL.Repositories.EntityRepositories;
using MusicStreamServiceApp.DAL.UnitOfWork;
using MusicStreamServiceApp.BLL.Services;
using MusicStreamServiceApp.BLL.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using MusicStreamServiceApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using MusicStreamServiceApp.BLL.Validation;
using AutoMapper;
using MusicStreamServiceApp.BLL;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System;
using MusicStreamServiceApp.BLL.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using MusicStreamServiceApp.API.Interface;
using MusicStreamServiceApp.API.Service;

namespace MusicStreamServiceApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MusicDBContext>(opts => opts.UseSqlServer(Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING")));

            services.AddControllers().AddFluentValidation();

            #region Repositories

            services.AddTransient<IMusicRepository, MusicRepository>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();
            services.AddTransient<IMusicGenreRepository, MusicGenreRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IMusicPlaylistRepository, MusicPlaylistRepository>();
            services.AddTransient<IUserPlaylistRepository, UserPlaylistRepository>();

            #endregion Repositories

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            #region Services

            services.AddTransient<IAccountService, AccountService>(); // Identity
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IPlaylistService, PlaylistService>();

            #endregion Services

            services.AddAutoMapper(typeof(MapperProfile));

            #region DTO Validators

            services.AddTransient<IValidator<MusicCUDTO>, MusicCUDTOValidator>();
            services.AddTransient<IValidator<GenreDTO>, GenreDTOValidator>();
            services.AddTransient<IValidator<AlbumDTO>, AlbumDTOValidator>();
            services.AddTransient<IValidator<PlaylistDTO>, PlaylistDTOValidator>();
            services.AddTransient<IValidator<MusicPlaylistDTO>, MusicPlaylistDTOValidator>();
            services.AddTransient<IValidator<PlaylistCUDTO>, PlaylistCUDTOValidator>();

            // Identity
            services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();
            services.AddTransient<IValidator<UserLoginDTO>, UserLoginDTOValidator>();
            services.AddTransient<IValidator<UserUpdateDTO>, UserUpdateDTOValidator>();

            #endregion DTO Validators

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MusicPlayer API",
                    Description = "ASP.NET Core Web API"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the bearer scheme"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });

                c.OperationFilter<AuthOperationFilter>();
            });

            #endregion Swagger

            #region Identity

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MusicDBContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Email settings.
                options.User.RequireUniqueEmail = true;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            #endregion Identity

            #region JWT Authentication

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer"),

                        ValidateAudience = true,
                        ValidAudience = Environment.GetEnvironmentVariable("JwtAudience"),

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey"))),

                        ValidateLifetime = false,

                        ClockSkew = TimeSpan.Zero,
                    };
                });

            services.AddTransient<IJwtTokenService, JwtTokenService>();

            #endregion JWT Authentication

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My MusicPlayer API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
