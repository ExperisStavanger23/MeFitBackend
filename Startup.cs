using System.Reflection;
using MeFitBackend.Data;
using MeFitBackend.Services.Exercises;
using MeFitBackend.Services.MuscleGroups;
using MeFitBackend.Services.Programs;
using MeFitBackend.Services.Users;
using MeFitBackend.Services.Workouts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MeFitBackend
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IProgramService, ProgramService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWorkoutService, WorkoutService>();
            builder.Services.AddScoped<IMuscleGroupService, MuscleGroupService>();
            builder.Services.AddCors();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSwagger",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            var test = FetchJwksAsync("https://lemur-10.cloud-iam.com/auth/realms/aiam/protocol/openid-connect/certs");
            System.Console.WriteLine(test.Result[0]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Configure the token validation parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "https://lemur-10.cloud-iam.com/auth/realms/aiam", // iss in token
                    ValidAudience = "account", // aud in token
                    IssuerSigningKey = test.Result[0], // singing key set (some/url/certs)
                    ValidateIssuer = true, // Validate the token's issuer
                    ValidateAudience = true, // Validate the token's audience
                    ValidateLifetime = true, // Check if the token is expired
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();


            builder.Services.AddDbContext<MeFitDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MeFitDb"));
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MeFitAPI",
                    Version = "v1",
                    Description = "An ASP.NET Core Web API for managing MeFit",
                });

                //// using System.Reflection;
                //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                // Add JWT Authentication support in Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // The name of the HTTP Authorization scheme to be used in the Swagger UI
                    BearerFormat = "JWT", // JWT format
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new List<string>() }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });





            var app = builder.Build();
            app.UseCors("AllowSwagger");

            // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.Run();

        }

        static async Task<SecurityKey[]> FetchJwksAsync(string jwksUri)
        {
            using (var httpClient = new HttpClient())
            {
                var jwksJson = await httpClient.GetStringAsync(jwksUri);

                // Parse the JWKS JSON and build the SecurityKey array
                var jwks = JsonWebKeySet.Create(jwksJson);
                return jwks.Keys.ToArray();
            }
        }
    }


}

