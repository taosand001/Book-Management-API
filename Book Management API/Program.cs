
using Book_Management_API.Database;
using Book_Management_API.Extension;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Repository;
using Book_Management_API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Book_Management_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<BookContext>(options =>
            {
                if (envName == "Production")
                {
                    var server = Environment.GetEnvironmentVariable("Server");
                    var userId = Environment.GetEnvironmentVariable("User_Id");
                    var password = Environment.GetEnvironmentVariable("Password");
                    var database = Environment.GetEnvironmentVariable("Database");
                    var port = Environment.GetEnvironmentVariable("Port");
                    var connectionString = $"Server={server};Database={database};User ID={userId};Password={password};Port={port};Trusted_Connection=True;MultipleActiveResultSets=true";
                    options.UseSqlServer(connectionString);
                }
                else if (envName == "Development")
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            });
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRecommendationService, RecommendationService>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Book Management API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddJwtExtension(builder.Configuration);
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsProduction())
            {
                using var scope = app.Services.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<BookContext>();
                db.Database.Migrate();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
