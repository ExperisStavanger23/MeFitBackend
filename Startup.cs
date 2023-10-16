using MeFitBackend.Data;
using MeFitBackend.Services.Exercises;
using MeFitBackend.Services.MuscleGroups;
using MeFitBackend.Services.Programs;
using MeFitBackend.Services.Users;
using MeFitBackend.Services.Workouts;
using Microsoft.EntityFrameworkCore;
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

            });





            var app = builder.Build();
            app.UseCors("AllowSwagger");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();

        }
    }
}