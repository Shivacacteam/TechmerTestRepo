namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_HSL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ColorSelection", "InternalHSL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ColorSelection", "InternalHSL");
        }
    }
}
