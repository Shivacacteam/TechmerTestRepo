namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Request_sample : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SampleRequestAssets", "Product_Id", "dbo.Product");
            DropIndex("dbo.SampleRequestAssets", new[] { "Product_Id" });
            CreateTable(
                "dbo.SampleRequestAsset",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssetType = c.String(),
                        Notes = c.String(),
                        Product_Id = c.Long(),
                        SampleRequest_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.SampleRequest", t => t.SampleRequest_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.SampleRequest_Id);
            
            CreateTable(
                "dbo.SampleRequest",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Owner = c.String(),
                        ProjectName = c.String(),
                        Notes = c.String(),
                        Status = c.Boolean(nullable: false),
                        SubmissionDate = c.DateTime(nullable: true),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: true),
                    })
                .PrimaryKey(t => t.Id);
            
           // DropTable("dbo.SampleRequestAssets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SampleRequestAssets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssetType = c.String(),
                        Note = c.String(),
                        Product_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.SampleRequestAsset", "SampleRequest_Id", "dbo.SampleRequest");
            DropForeignKey("dbo.SampleRequestAsset", "Product_Id", "dbo.Product");
            DropIndex("dbo.SampleRequestAsset", new[] { "SampleRequest_Id" });
            DropIndex("dbo.SampleRequestAsset", new[] { "Product_Id" });
            DropTable("dbo.SampleRequest");
            DropTable("dbo.SampleRequestAsset");
            CreateIndex("dbo.SampleRequestAssets", "Product_Id");
            AddForeignKey("dbo.SampleRequestAssets", "Product_Id", "dbo.Product", "Id");
        }
    }
}
