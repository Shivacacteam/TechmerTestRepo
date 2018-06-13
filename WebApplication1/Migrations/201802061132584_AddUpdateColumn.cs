namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdateColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ColorSelection", "InternalHSV", c => c.String());
            AddColumn("dbo.ColorSelection", "InternalCMYK", c => c.String());
            AddColumn("dbo.ColorSelection", "InternalHEX", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ColorSelection", "InternalHEX");
            DropColumn("dbo.ColorSelection", "InternalCMYK");
            DropColumn("dbo.ColorSelection", "InternalHSV");
        }
    }
}
