namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updaterequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SampleRequestAsset", "SampleRequest_Id", "dbo.SampleRequest");
            DropIndex("dbo.SampleRequestAsset", new[] { "SampleRequest_Id" });
            AddColumn("dbo.SampleRequestAsset", "RequestId", c => c.Long(nullable: false));
            DropColumn("dbo.SampleRequestAsset", "SampleRequest_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SampleRequestAsset", "SampleRequest_Id", c => c.Long());
            DropColumn("dbo.SampleRequestAsset", "RequestId");
            CreateIndex("dbo.SampleRequestAsset", "SampleRequest_Id");
            AddForeignKey("dbo.SampleRequestAsset", "SampleRequest_Id", "dbo.SampleRequest", "Id");
        }
    }
}
