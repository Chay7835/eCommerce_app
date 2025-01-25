using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhonecustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Phonenumber",
            //    table: "customers",
            //    newName: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Phone",
            //    table: "customers",
            //    newName: "Phonenumber");
        }
    }
}
