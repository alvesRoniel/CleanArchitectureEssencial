using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[PRODUCTS] " +
                                  "([Name]                          " +
                                  ",[Description]                   " +
                                  ",[Price]                         " +
                                  ",[Stok]                          " +
                                  ",[Image]                         " +
                                  ",[CategoryId]                    " +
                                  ",[CreatedDate]                   " +
                                  ",[ModifiedDate])                 " +
                                  " VALUES                          " +
                                  "('Caderno espiral',              " +
                                  "'Caderno esperial 100 folhas',   " +
                                  "7.45,                            " +
                                  "1,                               " +
                                  "'caderno1.png',                  " +
                                  "1,                               " +
                                  "getdate(),                       " +
                                  "null)                            " 
                                  );

            migrationBuilder.Sql("INSERT INTO [dbo].[PRODUCTS] " +
                                   "([Name]                          " +
                                   ",[Description]                   " +
                                   ",[Price]                         " +
                                   ",[Stok]                          " +
                                   ",[Image]                         " +
                                   ",[CategoryId]                    " +
                                   ",[CreatedDate]                   " +
                                   ",[ModifiedDate])                 " +
                                   " VALUES                          " +
                                   "('Estojo Escolar',               " +
                                   "'Estojo escolar cinza',          " +
                                   "5.45,                            " +
                                   "1,                               " +
                                   "'estojo1.png',                  " +
                                   "1,                               " +
                                   "getdate(),                       " +
                                   "null)                            "
                                   );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PRODUCTS");
        }
    }
}
