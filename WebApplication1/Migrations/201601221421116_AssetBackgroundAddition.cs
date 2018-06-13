namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetBackgroundAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "HasBackgroundImage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "BackgroundImage", c => c.String());
            AddColumn("dbo.ProductTemplate", "HasBackgroundImage", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductTemplate", "BackgroundImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTemplate", "BackgroundImage");
            DropColumn("dbo.ProductTemplate", "HasBackgroundImage");
            DropColumn("dbo.Product", "BackgroundImage");
            DropColumn("dbo.Product", "HasBackgroundImage");
        }
    }
}
