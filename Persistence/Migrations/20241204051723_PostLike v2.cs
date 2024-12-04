using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PostLikev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "PostAuthorId",
                table: "PostLike",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostAuthorId",
                table: "PostLike");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
