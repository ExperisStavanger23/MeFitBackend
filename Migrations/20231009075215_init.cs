using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeFitBackend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalType = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    RecommendedLevel = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MuscleGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MuscleGroup_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    RecommendedLevel = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workout_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceLvl = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutExercise_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Created",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Created", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Created_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExercise_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGoal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGoal_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGoal_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgram_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Program",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProgram_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkout_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkout_Workout_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Description", "Image", "Name", "Video" },
                values: new object[,]
                {
                    { 1, " Lay on your backon a flat bench, lower the barbell down in a slow pace to your chest level, and thenpress upwards by extending your arms.", "https://www.verywellfit.com/thmb/V4KJH4idbUskL-xSE85WSe8OsPA=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/man-training-with-weights-in-gym-147486767-c0eece2a50154d04ad521c1c3c391380.jpg", "Barbell Bench Press", "https://www.youtube.com/embed/rxD321l2svE" },
                    { 2, "Situps are classic abdominal exercises done by lying on your back and lifting your torso. They use your body weight to strengthen and tone the core-stabilizing abdominal muscles.", "https://images.healthshots.com/healthshots/en/uploads/2022/10/27130441/sit-ups-vs-crunches.jpg", "Situp", "https://www.youtube.com/embed=UMaZGY6CbC4" }
                });

            migrationBuilder.InsertData(
                table: "MuscleGroup",
                columns: new[] { "Id", "ExerciseId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Chest" },
                    { 2, null, "Triceps" },
                    { 3, null, "Shoulders" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "RoleTitle" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Contributer" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "Workout",
                columns: new[] { "Id", "Category", "Description", "Duration", "Image", "Name", "ProgramId", "RecommendedLevel" },
                values: new object[] { 1, 12, "Chest day is a day where you train your chest muscles", 60, "https://www.mensjournal.com/.image/t_share/MTk2MTM2NjcyOTc1NzI2MDg1/afitasianguyinawhitetanktopdoes.jpg", "Chest Day", null, 0 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Bio", "Birthday", "Email", "ExperienceLvl", "Gender", "Height", "Name", "ProfilePicture", "RoleId", "Weight" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1984), "jeffit@gmail.com", 2, "Male", 180, "Jeff", null, 1, 80 });

            migrationBuilder.InsertData(
                table: "WorkoutExercise",
                columns: new[] { "Id", "ExerciseId", "Reps", "Sets", "WorkoutId" },
                values: new object[] { 1, 1, 8, 4, 1 });

            migrationBuilder.InsertData(
                table: "Created",
                columns: new[] { "Id", "CreatorId", "EntityId", "EntityType" },
                values: new object[] { 1, 1, 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Created_CreatorId",
                table: "Created",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleGroup_ExerciseId",
                table: "MuscleGroup",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExercise_ExerciseId",
                table: "UserExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExercise_UserId",
                table: "UserExercise",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoal_GoalId",
                table: "UserGoal",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoal_UserId",
                table: "UserGoal",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgram_ProgramId",
                table: "UserProgram",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgram_UserId",
                table: "UserProgram",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkout_UserId",
                table: "UserWorkout",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkout_WorkoutId",
                table: "UserWorkout",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_ProgramId",
                table: "Workout",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_ExerciseId",
                table: "WorkoutExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercise_WorkoutId",
                table: "WorkoutExercise",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Created");

            migrationBuilder.DropTable(
                name: "MuscleGroup");

            migrationBuilder.DropTable(
                name: "UserExercise");

            migrationBuilder.DropTable(
                name: "UserGoal");

            migrationBuilder.DropTable(
                name: "UserProgram");

            migrationBuilder.DropTable(
                name: "UserWorkout");

            migrationBuilder.DropTable(
                name: "WorkoutExercise");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Program");
        }
    }
}
