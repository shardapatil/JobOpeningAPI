using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOpeningsAPI.Migrations
{
    /// <inheritdoc />
    public partial class jobopeningsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    state = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    zip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true, defaultValueSql: "CONCAT('JOB-', NEXT VALUE FOR JobCodeSequence)"),
                    title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    DepartmentDeptId = table.Column<int>(type: "int", nullable: true),
                    postedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    closingDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Job_Department_DepartmentDeptId",
                        column: x => x.DepartmentDeptId,
                        principalTable: "Department",
                        principalColumn: "DeptId");
                    table.ForeignKey(
                        name: "FK_Job_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_DepartmentDeptId",
                table: "Job",
                column: "DepartmentDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_LocationId",
                table: "Job",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
