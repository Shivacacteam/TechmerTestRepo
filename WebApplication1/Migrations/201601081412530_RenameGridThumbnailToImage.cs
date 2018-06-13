namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameGridThumbnailToImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grid", "Image", c => c.String());
            Sql("Update Grid Set Image = Thumbnail");
            DropColumn("dbo.Grid", "Thumbnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grid", "Thumbnail", c => c.String());
            Sql("Update Grid Set Thumbnail = Image");
            DropColumn("dbo.Grid", "Image");
        }
    }
}
