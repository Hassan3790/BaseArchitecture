using BaseArchitecture.Migrations.Scripts;
using FluentMigrator;

namespace BaseArchitecture.Infrastructures.Migrations.Migrations;

[Migration(202407150141)]
public class _202407150141_AddApplicationUserTable : Migration
{
    public override void Up()
    {
        var script = ScriptResourceManager.Read("aspidentity.sql");
        Execute.Sql(script);
    }

    public override void Down()
    {
        Delete.Table("AspNetUserTokens");
        Delete.Table("AspNetUserRoles");
        Delete.Table("AspNetUserLogins");
        Delete.Table("AspNetUserClaims");
        Delete.Table("AspNetRoleClaims");
        Delete.Table("AspNetRoles");
        Delete.Table("AspNetUsers");
    }
}