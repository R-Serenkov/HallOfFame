using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HallOfFame.Migrations
{
    /// <inheritdoc />
    public partial class Test_migration_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_Skill_SkillListId",
                table: "PersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "SkillListId",
                table: "PersonSkill",
                newName: "SkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSkill_SkillListId",
                table: "PersonSkill",
                newName: "IX_PersonSkill_SkillsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkill_Skills_SkillsId",
                table: "PersonSkill",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkill_Skills_SkillsId",
                table: "PersonSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "PersonSkill",
                newName: "SkillListId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSkill_SkillsId",
                table: "PersonSkill",
                newName: "IX_PersonSkill_SkillListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkill_Skill_SkillListId",
                table: "PersonSkill",
                column: "SkillListId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
