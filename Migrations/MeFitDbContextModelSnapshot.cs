﻿// <auto-generated />
using System;
using MeFitBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeFitBackend.Migrations
{
    [DbContext(typeof(MeFitDbContext))]
    partial class MeFitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeFitBackend.Data.Entities.Created", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("CreatorId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<int>("EntityType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId1");

                    b.ToTable("Created");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatorId = 1,
                            EntityId = 1,
                            EntityType = 0
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Exercise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = " Lay on your backon a flat bench, lower the barbell down in a slow pace to your chest level, and thenpress upwards by extending your arms.",
                            Image = "https://www.verywellfit.com/thmb/V4KJH4idbUskL-xSE85WSe8OsPA=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/man-training-with-weights-in-gym-147486767-c0eece2a50154d04ad521c1c3c391380.jpg",
                            Name = "Barbell Bench Press",
                            Video = "https://www.youtube.com/embed/rxD321l2svE"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Situps are classic abdominal exercises done by lying on your back and lifting your torso. They use your body weight to strengthen and tone the core-stabilizing abdominal muscles.",
                            Image = "https://images.healthshots.com/healthshots/en/uploads/2022/10/27130441/sit-ups-vs-crunches.jpg",
                            Name = "Situp",
                            Video = "https://www.youtube.com/embed/UMaZGY6CbC4"
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.ExerciseMuscleGroup", b =>
                {
                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("MuscleGroupId")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId", "MuscleGroupId");

                    b.HasIndex("MuscleGroupId");

                    b.ToTable("ExerciseMuscleGroup");

                    b.HasData(
                        new
                        {
                            ExerciseId = 1,
                            MuscleGroupId = 1
                        },
                        new
                        {
                            ExerciseId = 1,
                            MuscleGroupId = 2
                        },
                        new
                        {
                            ExerciseId = 1,
                            MuscleGroupId = 3
                        },
                        new
                        {
                            ExerciseId = 2,
                            MuscleGroupId = 13
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.MuscleGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MuscleGroup");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Chest"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Triceps"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Shoulders"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Upper Back"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Lower Back"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Biceps"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Triceps"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Quads"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Hamstrings"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Calves"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Glutes"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Hips"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Abs"
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RecommendedLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Program");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 1,
                            Description = "Get bigger upper body",
                            Duration = 14,
                            Image = "https://media.gq-magazine.co.uk/photos/63d3b425a8dee570a85a25f1/16:9/w_2560%2Cc_limit/Workout-HEADER.jpg",
                            Name = "Upper Body Program",
                            RecommendedLevel = 0
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleTitle = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleTitle = "Contributor"
                        },
                        new
                        {
                            Id = 3,
                            RoleTitle = "User"
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bio")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ExperienceLvl")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutGoal")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Birthday = new DateTime(1999, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jeffit@gmail.com",
                            ExperienceLvl = 2,
                            Gender = "Male",
                            Height = 180,
                            Name = "Jeff",
                            Weight = 80,
                            WorkoutGoal = 0
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExercise");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProgram");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserWorkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DoneDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("UserWorkout");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProgramId")
                        .HasColumnType("int");

                    b.Property<int>("RecommendedLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.ToTable("Workout");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 12,
                            Description = "Chest day is a day where you train your chest muscles",
                            Duration = 60,
                            Image = "https://www.mensjournal.com/.image/t_share/MTk2MTM2NjcyOTc1NzI2MDg1/afitasianguyinawhitetanktopdoes.jpg",
                            Name = "Chest Day",
                            RecommendedLevel = 0
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.WorkoutExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Sets")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("WorkoutExercise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExerciseId = 1,
                            Reps = 8,
                            Sets = 4,
                            WorkoutId = 1
                        });
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Created", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId1");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.ExerciseMuscleGroup", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.Exercise", "Exercise")
                        .WithMany("ExerciseMuscleGroups")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFitBackend.Data.Entities.MuscleGroup", "MuscleGroup")
                        .WithMany("ExerciseMuscleGroups")
                        .HasForeignKey("MuscleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("MuscleGroup");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserExercise", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.Exercise", "Exercise")
                        .WithMany("UserExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFitBackend.Data.Entities.User", "User")
                        .WithMany("UserExercises")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserProgram", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.Program", "Program")
                        .WithMany("UserPrograms")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFitBackend.Data.Entities.User", null)
                        .WithMany("UserPrograms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserRole", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFitBackend.Data.Entities.User", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.UserWorkout", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.User", null)
                        .WithMany("UserWorkouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFitBackend.Data.Entities.Workout", "Workout")
                        .WithMany("UserWorkouts")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Workout", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.Program", null)
                        .WithMany("Workout")
                        .HasForeignKey("ProgramId");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.WorkoutExercise", b =>
                {
                    b.HasOne("MeFitBackend.Data.Entities.Exercise", "Exercise")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeFitBackend.Data.Entities.Workout", "Workout")
                        .WithMany("WorkoutExercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Exercise", b =>
                {
                    b.Navigation("ExerciseMuscleGroups");

                    b.Navigation("UserExercises");

                    b.Navigation("WorkoutExercises");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.MuscleGroup", b =>
                {
                    b.Navigation("ExerciseMuscleGroups");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Program", b =>
                {
                    b.Navigation("UserPrograms");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.User", b =>
                {
                    b.Navigation("UserExercises");

                    b.Navigation("UserPrograms");

                    b.Navigation("UserRoles");

                    b.Navigation("UserWorkouts");
                });

            modelBuilder.Entity("MeFitBackend.Data.Entities.Workout", b =>
                {
                    b.Navigation("UserWorkouts");

                    b.Navigation("WorkoutExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
