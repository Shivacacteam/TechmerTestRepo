namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GridCompareDataModelAdditions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workspace", "x", c => c.Int(nullable: false));
            AddColumn("dbo.Workspace", "y", c => c.Int(nullable: false));
            AddColumn("dbo.Workspace", "s", c => c.Int(nullable: false));
            AddColumn("dbo.Workspace", "r", c => c.Int(nullable: false));
            AddColumn("dbo.Workspace", "zIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Grid", "spacing", c => c.Int(nullable: false));
            AddColumn("dbo.Grid", "x", c => c.Int(nullable: false));
            AddColumn("dbo.Grid", "y", c => c.Int(nullable: false));
            AddColumn("dbo.Grid", "s", c => c.Int(nullable: false));
            AddColumn("dbo.Grid", "r", c => c.Int(nullable: false));
            AddColumn("dbo.Grid", "zIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "x", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "y", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "s", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "r", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "zIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "zIndex");
            DropColumn("dbo.Product", "r");
            DropColumn("dbo.Product", "s");
            DropColumn("dbo.Product", "y");
            DropColumn("dbo.Product", "x");
            DropColumn("dbo.Grid", "zIndex");
            DropColumn("dbo.Grid", "r");
            DropColumn("dbo.Grid", "s");
            DropColumn("dbo.Grid", "y");
            DropColumn("dbo.Grid", "x");
            DropColumn("dbo.Grid", "spacing");
            DropColumn("dbo.Workspace", "zIndex");
            DropColumn("dbo.Workspace", "r");
            DropColumn("dbo.Workspace", "s");
            DropColumn("dbo.Workspace", "y");
            DropColumn("dbo.Workspace", "x");
        }
    }
}
