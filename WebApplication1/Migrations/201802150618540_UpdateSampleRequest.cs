namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSampleRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SampleRequestAsset", "Product_Id", "dbo.Product");
            DropIndex("dbo.SampleRequestAsset", new[] { "Product_Id" });
            AddColumn("dbo.SampleRequestAsset", "AssetId", c => c.Long(nullable: false));
            DropColumn("dbo.SampleRequestAsset", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SampleRequestAsset", "Product_Id", c => c.Long());
            DropColumn("dbo.SampleRequestAsset", "AssetId");
            CreateIndex("dbo.SampleRequestAsset", "Product_Id");
            AddForeignKey("dbo.SampleRequestAsset", "Product_Id", "dbo.Product", "Id");
        }
    }
}
