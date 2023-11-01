using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReelTalkReviews.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmCertification",
                columns: table => new
                {
                    FilmCertificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmCertificationType = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Definition = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FilmCert__D924180A79F8CC4D", x => x.FilmCertificationId);
                });

            migrationBuilder.CreateTable(
                name: "FilmIndustryMember",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MemberPic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    MemberDescription = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Place = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    IsDirector = table.Column<bool>(type: "bit", nullable: true),
                    IsMusicDirector = table.Column<bool>(type: "bit", nullable: true),
                    IsActor = table.Column<bool>(type: "bit", nullable: true),
                    IsProducer = table.Column<bool>(type: "bit", nullable: true),
                    IsCinematographer = table.Column<bool>(type: "bit", nullable: true),
                    IsWriter = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FilmIndu__0CF04B189FECCB58", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE1A1E090515", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "MovieDetail",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTitle = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MovieType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MoviePoster = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MovieRatingOverall = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    FilmCertificationId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "date", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MovieDet__4BD2941ABBBD5697", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK__MovieDeta__FilmC__6E01572D",
                        column: x => x.FilmCertificationId,
                        principalTable: "FilmCertification",
                        principalColumn: "FilmCertificationId");
                });

            migrationBuilder.CreateTable(
                name: "UserDetail",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "date", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "date", nullable: true),
                    DisplayPic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "date", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordTokenExpiry = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserData__1788CC4C7EA3C4DE", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__UserDetai__RoleI__3C34F16F",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "CastDetail",
                columns: table => new
                {
                    CastId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    LeadActorId = table.Column<int>(type: "int", nullable: true),
                    LeadActor1As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeadActorId2 = table.Column<int>(type: "int", nullable: true),
                    LeadActor2As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeadActorId3 = table.Column<int>(type: "int", nullable: true),
                    LeadActor3As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeadActorId4 = table.Column<int>(type: "int", nullable: true),
                    LeadActor4As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeadActorId5 = table.Column<int>(type: "int", nullable: true),
                    LeadActor5As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeadActorId6 = table.Column<int>(type: "int", nullable: true),
                    LeadActor6As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LeadActorId7 = table.Column<int>(type: "int", nullable: true),
                    LeadActor7As = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CastData__68A1293C10E34199", x => x.CastId);
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__2180FB33",
                        column: x => x.LeadActorId,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__22751F6C",
                        column: x => x.LeadActorId2,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__236943A5",
                        column: x => x.LeadActorId3,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__245D67DE",
                        column: x => x.LeadActorId4,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__25518C17",
                        column: x => x.LeadActorId5,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__2645B050",
                        column: x => x.LeadActorId6,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__LeadAc__2739D489",
                        column: x => x.LeadActorId7,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__CastData__MovieI__208CD6FA",
                        column: x => x.MovieId,
                        principalTable: "MovieDetail",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "Crew",
                columns: table => new
                {
                    CrewId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    Director = table.Column<int>(type: "int", nullable: true),
                    MusicDirector = table.Column<int>(type: "int", nullable: true),
                    Producer = table.Column<int>(type: "int", nullable: true),
                    Cinematographer = table.Column<int>(type: "int", nullable: true),
                    Writer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Crew__89BCFC2982A905CD", x => x.CrewId);
                    table.ForeignKey(
                        name: "FK__Crew__Cinematogr__42E1EEFE",
                        column: x => x.Cinematographer,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__Crew__Director__40058253",
                        column: x => x.Director,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__Crew__MovieId__3F115E1A",
                        column: x => x.MovieId,
                        principalTable: "MovieDetail",
                        principalColumn: "MovieId");
                    table.ForeignKey(
                        name: "FK__Crew__MusicDirec__40F9A68C",
                        column: x => x.MusicDirector,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__Crew__Producer__41EDCAC5",
                        column: x => x.Producer,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK__Crew__Writer__43D61337",
                        column: x => x.Writer,
                        principalTable: "FilmIndustryMember",
                        principalColumn: "MemberId");
                });

            migrationBuilder.CreateTable(
                name: "MoviePhotos",
                columns: table => new
                {
                    MoviePicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    MoviePhotos = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MoviePho__61D4B0C1799619EC", x => x.MoviePicId);
                    table.ForeignKey(
                        name: "FK__MoviePhot__Movie__756D6ECB",
                        column: x => x.MovieId,
                        principalTable: "MovieDetail",
                        principalColumn: "MovieId");
                });

            migrationBuilder.CreateTable(
                name: "MovieRating",
                columns: table => new
                {
                    MovieRatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    review = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MovieRat__AB2CC873E857B0AF", x => x.MovieRatingId);
                    table.ForeignKey(
                        name: "FK__MovieRati__Movie__797309D9",
                        column: x => x.MovieId,
                        principalTable: "MovieDetail",
                        principalColumn: "MovieId");
                    table.ForeignKey(
                        name: "FK__MovieRati__UserI__7A672E12",
                        column: x => x.UserId,
                        principalTable: "UserDetail",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId",
                table: "CastDetail",
                column: "LeadActorId");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId2",
                table: "CastDetail",
                column: "LeadActorId2");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId3",
                table: "CastDetail",
                column: "LeadActorId3");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId4",
                table: "CastDetail",
                column: "LeadActorId4");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId5",
                table: "CastDetail",
                column: "LeadActorId5");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId6",
                table: "CastDetail",
                column: "LeadActorId6");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_LeadActorId7",
                table: "CastDetail",
                column: "LeadActorId7");

            migrationBuilder.CreateIndex(
                name: "IX_CastDetail_MovieId",
                table: "CastDetail",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_Cinematographer",
                table: "Crew",
                column: "Cinematographer");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_Director",
                table: "Crew",
                column: "Director");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_MovieId",
                table: "Crew",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_MusicDirector",
                table: "Crew",
                column: "MusicDirector");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_Producer",
                table: "Crew",
                column: "Producer");

            migrationBuilder.CreateIndex(
                name: "IX_Crew_Writer",
                table: "Crew",
                column: "Writer");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetail_FilmCertificationId",
                table: "MovieDetail",
                column: "FilmCertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePhotos_MovieId",
                table: "MoviePhotos",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRating_MovieId",
                table: "MovieRating",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRating_UserId",
                table: "MovieRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_RoleId",
                table: "UserDetail",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__UserData__C9F28456C335EE04",
                table: "UserDetail",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastDetail");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropTable(
                name: "MoviePhotos");

            migrationBuilder.DropTable(
                name: "MovieRating");

            migrationBuilder.DropTable(
                name: "FilmIndustryMember");

            migrationBuilder.DropTable(
                name: "MovieDetail");

            migrationBuilder.DropTable(
                name: "UserDetail");

            migrationBuilder.DropTable(
                name: "FilmCertification");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
