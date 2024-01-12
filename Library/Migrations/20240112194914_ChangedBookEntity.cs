using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class ChangedBookEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ec94c49b-fd26-4809-a471-4f3ed365db1c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "44fc5936-cbce-4343-807d-3a8e026cdd08");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4b77fc8d-c8fc-49e5-b211-d1db62ead86f", "AQAAAAEAACcQAAAAECYP9cnvtMyKh1zzjvijrAULYrDTOb30x+zoUb0j7aiofPnFdFSQUDehzG9QQOFsuQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0240c9bc-1578-4b29-b44b-021cc2b59f1f", "AQAAAAEAACcQAAAAEP/WI6E6L8JKdVWrQ0VhXOGk8FwVCyc/hnyj8F9CwosP+9HJZjOoaEwj1n7zDs7ChA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "18c4e1eb-3fba-4e0e-9176-a605c1eb6530");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2251e415-d10c-4d4d-affe-32e8ab39f83d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1fe86725-cad1-4ab2-94c7-5f95f31f5237", "AQAAAAEAACcQAAAAEPQw1Lj0RTvh3tf3gF/Qc5HuO413cnAPafmz5CbXy6HIfz8QlgF/AfK5VAX3hegHrQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05043b4c-04b1-4023-8212-3444d4ecb2fb", "AQAAAAEAACcQAAAAENe2pKPjWPzxb7EzkGg8Q9fH4+kZK/vcswkIRA3v8+gP9QpbfMgJaZqpnKkEpLrX/Q==" });
        }
    }
}
