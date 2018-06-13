#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;


    public partial class AddAssetFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workspace", "Filename", c => c.String());
            Sql("Update Workspace Set Filename = Id");
            AddColumn("dbo.Grid", "Filename", c => c.String());
            Sql("Update Grid Set Filename = Id");
            AddColumn("dbo.Product", "Filename", c => c.String());
            Sql("Update Product Set Filename = Id");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Filename");
            DropColumn("dbo.Grid", "Filename");
            DropColumn("dbo.Workspace", "Filename");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member