namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTemplate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTemplate", "Sharing_Id", "dbo.Sharing");
            DropIndex("dbo.ProductTemplate", new[] { "Sharing_Id" });
            DropColumn("dbo.ProductTemplate", "Sharing_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTemplate", "Sharing_Id", c => c.Long());
            CreateIndex("dbo.ProductTemplate", "Sharing_Id");
            AddForeignKey("dbo.ProductTemplate", "Sharing_Id", "dbo.Sharing", "Id");
        }
    }
}
