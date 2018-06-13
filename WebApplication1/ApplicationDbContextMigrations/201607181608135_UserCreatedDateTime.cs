#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace TechmerVision.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCreatedDateTime : DbMigration
    {

        public override void Up()

        {
            AddColumn("dbo.AspNetUsers", "CreatedDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2", defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CreatedDateTime");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member