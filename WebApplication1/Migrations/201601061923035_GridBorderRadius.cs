namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GridBorderRadius : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grid", "borderRadius", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grid", "borderRadius");
        }
    }
}
