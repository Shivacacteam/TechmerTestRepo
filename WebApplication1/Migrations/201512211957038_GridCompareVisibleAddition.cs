namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GridCompareVisibleAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workspace", "compareVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Grid", "compareVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "compareVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "compareVisible");
            DropColumn("dbo.Grid", "compareVisible");
            DropColumn("dbo.Workspace", "compareVisible");
        }
    }
}
