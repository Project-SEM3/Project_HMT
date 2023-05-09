using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMT.Migrations
{
    /// <inheritdoc />
    public partial class fielCodeRequest_Requests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeRequest",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2bb5675c-565c-4e3a-8b57-34a203f40ee6", "AQAAAAIAAYagAAAAEPLx/HybWX8NYWsDE6f9Y1C6L/v3Myu2wd86hsMCcVGQDQT2GNpeXzuHw5NUp/zvfg==", "b43aaa73-c025-4646-a245-7c12db284cb4", "Miranda" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "6673c64e-9082-499b-9cfb-a88579f58d7f", "AQAAAAIAAYagAAAAEFGFmjuPXOQ85Mimslbm3W7Lsmh7D9uEyK17iY3GkDzIbWpTyqM5N2dy/m3r9+u0mA==", "6722fdc7-d150-4dcd-9a96-3aafedb09b9b", "LukeIvory" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b3e3584c-7fef-4b20-9b09-fe0ee560027a", "AQAAAAIAAYagAAAAEK6KUFSqgYlTKJWk3mQsqN34DEN4t0xdtUJbj7qGJVC0DAyykQjdQ13hrgO0fVSZpQ==", "ee70f397-3543-4859-81ac-12e04a46f501", "AndyKing" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "93818a95-e3ef-4fa5-9d54-bbf7623086cd", "AQAAAAIAAYagAAAAEOJYVHISqaKSdILRXzZXB8Qdx2Nc4PiZgys3e7JOq6bvfvsoKFNlzBC0UJx/Px5xWQ==", "f0073f30-585b-4bcf-871a-f7940ee47e2a", "IreneCollins" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f5453bd8-1e64-4a94-9475-b90d98edf45c", "AQAAAAIAAYagAAAAEIkPTae3V0NOn/rCtm2E1UvjdIx0DauA/t4zVpmbJY9PvWlz+V5yCBcoWSZOjvZy+w==", "957d7940-402d-46e5-8885-59870f114662", "LaurieFox" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeRequest",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "df1daec9-3390-4a24-a338-78fa632cdd20", "AQAAAAIAAYagAAAAEHwqVf134pF6GYa4lEel/HgcBVtayDUbWt2mZxPL2TQWCOpVO6twH11XX9BZW9gEYQ==", "a830f47c-6174-4e28-adb7-6daf939d1705", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "6d83dea8-6e01-497c-9785-b1bc6c954bcd", "AQAAAAIAAYagAAAAEFdadZ7GZRXb90aNC/ENAA7ErlUlsos3Av3rOi/PZYfpPArqUc8eMmZ5IYudpH7CrQ==", "bc554a2e-853c-4319-8744-0621427740e7", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "fc31312d-fe19-49bc-b1e0-e0c7138beeb0", "AQAAAAIAAYagAAAAEJskerV+4GUWratBAkZ0FKBkCpa1157FE4Lplp91uPNyVVzZ4s8+u9vyNPxVAkANWQ==", "7658a695-311f-4f5b-9bca-d85df35108c0", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "7170f69d-384c-4721-af5c-5244c00556f0", "AQAAAAIAAYagAAAAEPeILHOHctNhw0n5Jr3sVMYO36+2XUWc+NLt42CangvFScBa/yb8upC4/KtO8/GpXA==", "e319926f-d428-429e-979f-bb54978d03e3", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "b91a4bda-e99b-4a16-8d37-144dbc529247", "AQAAAAIAAYagAAAAENdVo0dVOqiL8rM9hGHxWYNlTfpzolox84REnJzuK9UAeeETGAl1OGW+u6+NWPTXvA==", "c7ce01e8-6cab-482a-8a22-f25b7047c049", null });
        }
    }
}
