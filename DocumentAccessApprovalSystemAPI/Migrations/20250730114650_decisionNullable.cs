﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentAccessApprovalSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class decisionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRequests_Decisions_DecisionId",
                table: "AccessRequests");

            migrationBuilder.AlterColumn<int>(
                name: "DecisionId",
                table: "AccessRequests",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRequests_Decisions_DecisionId",
                table: "AccessRequests",
                column: "DecisionId",
                principalTable: "Decisions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRequests_Decisions_DecisionId",
                table: "AccessRequests");

            migrationBuilder.AlterColumn<int>(
                name: "DecisionId",
                table: "AccessRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRequests_Decisions_DecisionId",
                table: "AccessRequests",
                column: "DecisionId",
                principalTable: "Decisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
