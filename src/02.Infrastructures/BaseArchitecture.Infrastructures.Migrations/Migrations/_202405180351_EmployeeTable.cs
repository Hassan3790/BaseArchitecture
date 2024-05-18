using FluentMigrator;

namespace test.Migrations;

[Migration(202405180351)]
public class _202405180351_AddEmployeeTable : Migration
{
    public override void Up()
    {
        Create.Table("Employees")
            .WithColumn("Id").AsCustom("VARCHAR(255)").PrimaryKey()
            .WithColumn("FirstName").AsString(500).NotNullable()
            .WithColumn("LastName").AsString(500).NotNullable()
            .WithColumn("NationalCode").AsCustom("VARCHAR(10)").NotNullable()
            .WithColumn("PhoneNumber").AsCustom("VARCHAR(15)").NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Employees");
    }
}