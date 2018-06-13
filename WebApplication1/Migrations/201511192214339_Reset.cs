namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ColorSelection",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkspaceId = c.Long(nullable: false),
                        TimeStamp = c.Long(nullable: false),
                        InternalColorString = c.String(),
                        InternalHSL = c.String(),
                        ColorStyle = c.String(),
                        ColorString = c.String(),
                        Favorite = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workspace", t => t.WorkspaceId, cascadeDelete: true)
                .Index(t => t.WorkspaceId);
            
            CreateTable(
                "dbo.Workspace",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        Image = c.String(),
                        Pixelation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Grid",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkspaceId = c.Long(nullable: false),
                        ModifiedTimeStamp = c.Long(nullable: false),
                        InternalTopLeftColorString = c.String(),
                        InternalTopRightColorString = c.String(),
                        InternalBottomLeftColorString = c.String(),
                        InternalBottomRightColorString = c.String(),
                        HorizontalWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VerticalWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Thumbnail = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workspace", t => t.WorkspaceId, cascadeDelete: true)
                .Index(t => t.WorkspaceId);
            
            CreateTable(
                "dbo.ProductColor",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        ColorNumber = c.Int(nullable: false),
                        InternalColorString = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WorkspaceId = c.Long(nullable: false),
                        ProductTemplateId = c.Long(nullable: false),
                        Title = c.String(),
                        Image = c.String(),
                        ModifiedTimeStamp = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTemplate", t => t.ProductTemplateId, cascadeDelete: true)
                .ForeignKey("dbo.Workspace", t => t.WorkspaceId, cascadeDelete: true)
                .Index(t => t.WorkspaceId)
                .Index(t => t.ProductTemplateId);
            
            CreateTable(
                "dbo.ProductTemplate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        NumColors = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductTemplateColor",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductTemplateId = c.Long(nullable: false),
                        ColorNumber = c.Int(nullable: false),
                        InternalColorString = c.String(),
                        RecolorToleranceUpperLimit = c.Int(nullable: false),
                        RecolorToleranceLowerLimit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTemplate", t => t.ProductTemplateId, cascadeDelete: true)
                .Index(t => t.ProductTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Product", "ProductTemplateId", "dbo.ProductTemplate");
            DropForeignKey("dbo.ProductTemplateColor", "ProductTemplateId", "dbo.ProductTemplate");
            DropForeignKey("dbo.ProductColor", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace");
            DropIndex("dbo.ProductTemplateColor", new[] { "ProductTemplateId" });
            DropIndex("dbo.Product", new[] { "ProductTemplateId" });
            DropIndex("dbo.Product", new[] { "WorkspaceId" });
            DropIndex("dbo.ProductColor", new[] { "ProductId" });
            DropIndex("dbo.Grid", new[] { "WorkspaceId" });
            DropIndex("dbo.ColorSelection", new[] { "WorkspaceId" });
            DropTable("dbo.ProductTemplateColor");
            DropTable("dbo.ProductTemplate");
            DropTable("dbo.Product");
            DropTable("dbo.ProductColor");
            DropTable("dbo.Grid");
            DropTable("dbo.Workspace");
            DropTable("dbo.ColorSelection");
        }
    }
}
