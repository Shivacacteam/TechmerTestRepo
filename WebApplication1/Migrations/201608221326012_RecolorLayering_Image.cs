#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecolorLayering_Image : DbMigration
    { 
        public override void Up()
        {
            AddColumn("dbo.ProductColor", "Image", c => c.String());
            AddColumn("dbo.ProductTemplateColor", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTemplateColor", "Image");
            DropColumn("dbo.ProductColor", "Image");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member