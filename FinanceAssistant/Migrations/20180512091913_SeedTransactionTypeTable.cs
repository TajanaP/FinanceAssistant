using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinanceAssistant.Migrations
{
    public partial class SeedTransactionTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TransactionTypes (Name) VALUES ('Expense')");
            migrationBuilder.Sql("INSERT INTO TransactionTypes (Name) VALUES ('Income')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TransactionTypes WHERE Name IN ('Expense', 'Income')");
        }
    }
}
