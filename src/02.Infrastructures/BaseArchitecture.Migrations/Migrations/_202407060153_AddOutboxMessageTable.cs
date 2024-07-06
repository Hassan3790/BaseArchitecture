using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace BaseArchitecture.Migrations.Migrations
{
    [Migration(202407060153)]
    public class _202407060153_AddOutboxMessageTable : Migration
    {
        public override void Up()
        {
            Create.Table("OutboxMessages")
                .WithColumn("Id").AsCustom("VARCHAR(255)").PrimaryKey()
                .WithColumn("Type").AsString(455).NotNullable()
                .WithColumn("Content").AsString(int.MaxValue).NotNullable()
                .WithColumn("OccurredTime").AsDateTime().NotNullable()
                .WithColumn("PublishedTime").AsDateTime().Nullable()
                .WithColumn("Error").AsString(4000).Nullable();
        }

        public override void Down()
        {
            Delete.Table("OutboxMessages");
        }
    }
}
