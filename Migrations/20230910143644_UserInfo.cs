using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeretTune.Migrations
{
    /// <inheritdoc />
    public partial class UserInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumUserInfo_UserInfo_BoughtByUserIdUserInfoId",
                table: "AlbumUserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUserInfo_UserInfo_FollowersUserInfoId",
                table: "ArtistUserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserInfo_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_UserInfo_UserId",
                table: "Playlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.RenameTable(
                name: "UserInfo",
                newName: "UserInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumUserInfo_UserInfos_BoughtByUserIdUserInfoId",
                table: "AlbumUserInfo",
                column: "BoughtByUserIdUserInfoId",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUserInfo_UserInfos_FollowersUserInfoId",
                table: "ArtistUserInfo",
                column: "FollowersUserInfoId",
                principalTable: "UserInfos",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumUserInfo_UserInfos_BoughtByUserIdUserInfoId",
                table: "AlbumUserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistUserInfo_UserInfos_FollowersUserInfoId",
                table: "ArtistUserInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserInfos_Id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_UserInfos_UserId",
                table: "Playlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "UserInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumUserInfo_UserInfo_BoughtByUserIdUserInfoId",
                table: "AlbumUserInfo",
                column: "BoughtByUserIdUserInfoId",
                principalTable: "UserInfo",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistUserInfo_UserInfo_FollowersUserInfoId",
                table: "ArtistUserInfo",
                column: "FollowersUserInfoId",
                principalTable: "UserInfo",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserInfo_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "UserInfo",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_UserInfo_UserId",
                table: "Playlist",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "UserInfoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
