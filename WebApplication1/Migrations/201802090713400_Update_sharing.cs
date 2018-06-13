namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_sharing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTemplate", "Sharing_Id", c => c.Long());
            CreateIndex("dbo.ProductTemplate", "Sharing_Id");
            AddForeignKey("dbo.ProductTemplate", "Sharing_Id", "dbo.Sharing", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTemplate", "Sharing_Id", "dbo.Sharing");
            DropIndex("dbo.ProductTemplate", new[] { "Sharing_Id" });
            DropColumn("dbo.ProductTemplate", "Sharing_Id");
        }
    }
}
