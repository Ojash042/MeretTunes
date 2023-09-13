using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeretTune.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserInfos_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_UserInfos_UserId",
                table: "Playlist");

            migrationBuilder.DropTable(
                name: "AlbumUserInfo");

            migrationBuilder.DropTable(
                name: "ArtistUserInfo");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileImage",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlbumUsers",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumUsers", x => new { x.ApplicationUserId, x.AlbumId });
                    table.ForeignKey(
                        name: "FK_AlbumUsers_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumUsers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistFollowers",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistFollowers", x => new { x.ApplicationUserId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_ArtistFollowers_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistFollowers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumUsers_AlbumId",
                table: "AlbumUsers",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistFollowers_ArtistId",
                table: "ArtistFollowers",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_AspNetUsers_UserId",
                table: "Playlist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_AspNetUsers_UserId",
                table: "Playlist");

            migrationBuilder.DropTable(
                name: "AlbumUsers");

            migrationBuilder.DropTable(
                name: "ArtistFollowers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserProfileImage",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    UserProfileImage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserInfoId);
                });

            migrationBuilder.CreateTable(
                name: "AlbumUserInfo",
                columns: table => new
                {
                    BoughtAlbumsAlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    BoughtByUserIdUserInfoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumUserInfo", x => new { x.BoughtAlbumsAlbumId, x.BoughtByUserIdUserInfoId });
                    table.ForeignKey(
                        name: "FK_AlbumUserInfo_Albums_BoughtAlbumsAlbumId",
                        column: x => x.BoughtAlbumsAlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumUserInfo_UserInfos_BoughtByUserIdUserInfoId",
                        column: x => x.BoughtByUserIdUserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistUserInfo",
                columns: table => new
                {
                    FollowedArtistsArtistId = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowersUserInfoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistUserInfo", x => new { x.FollowedArtistsArtistId, x.FollowersUserInfoId });
                    table.ForeignKey(
                        name: "FK_ArtistUserInfo_Artists_FollowedArtistsArtistId",
                        column: x => x.FollowedArtistsArtistId,
                        principalTable: "Artists",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistUserInfo_UserInfos_FollowersUserInfoId",
                        column: x => x.FollowersUserInfoId,
                        principalTable: "UserInfos",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumUserInfo_BoughtByUserIdUserInfoId",
                table: "AlbumUserInfo",
                column: "BoughtByUserIdUserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistUserInfo_FollowersUserInfoId",
                table: "ArtistUserInfo",
                column: "FollowersUserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserInfos_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_UserInfos_UserId",
                table: "Playlist",
                column: "UserId",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
