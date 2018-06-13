namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSharing_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sharing",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        AssetType = c.String(),
                        AssetId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductTemplate", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTemplate", "Owner");
            DropTable("dbo.Sharing");
        }
    }
}
