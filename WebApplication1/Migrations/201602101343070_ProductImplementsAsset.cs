namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImplementsAsset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductColor", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductColor", new[] { "ProductId" });
            DropPrimaryKey("dbo.ProductColor");
            DropPrimaryKey("dbo.Product");
            AddColumn("dbo.Product", "zIndexTimeStamp", c => c.Long(nullable: false));

            RenameColumn("dbo.Product", "Id", "IdOld");
            RenameColumn("dbo.ProductColor", "Id", "IdOld");

            AddColumn("dbo.ProductColor", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddColumn("dbo.Product", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));

            RenameColumn("dbo.ProductColor", "ProductId", "ProductIdOld");
            AddColumn("dbo.ProductColor", "ProductId", c => c.Guid(nullable: false));
            Sql(@"
                UPDATE dbo.ProductColor
                SET ProductColor.ProductId = Product.Id
                FROM ProductColor
                    Inner Join Product
                        On ProductColor.ProductIdOld = Product.IdOld
            ");

            DropColumn("dbo.Product", "IdOld");
            DropColumn("dbo.ProductColor", "IdOld");
            DropColumn("dbo.ProductColor", "ProductIdOld");

            AddPrimaryKey("dbo.ProductColor", "Id");
            AddPrimaryKey("dbo.Product", "Id");
            CreateIndex("dbo.ProductColor", "ProductId");
            AddForeignKey("dbo.ProductColor", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColor", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductColor", new[] { "ProductId" });
            DropPrimaryKey("dbo.Product");
            DropPrimaryKey("dbo.ProductColor");

            RenameColumn("dbo.Product", "Id", "IdOld");
            AddColumn("dbo.Product", "Id", c => c.Long(nullable: false, identity: true));

            RenameColumn("dbo.ProductColor", "ProductId", "ProductIdOld");
            AddColumn("dbo.ProductColor", "ProductId", c => c.Long(nullable: false));

            RenameColumn("dbo.ProductColor", "Id", "IdOld");
            AddColumn("dbo.ProductColor", "Id", c => c.Long(nullable: false, identity: true));
            Sql(@"
                UPDATE dbo.ProductColor
                SET ProductColor.ProductId = Product.Id
                FROM ProductColor
                    Inner Join Product
                        On ProductColor.ProductIdOld = Product.IdOld
            ");

            DropColumn("dbo.Product", "zIndexTimeStamp");
            DropColumn("dbo.Product", "IdOld");
            DropColumn("dbo.ProductColor", "IdOld");
            DropColumn("dbo.ProductColor", "ProductIdOld");

            AddPrimaryKey("dbo.Product", "Id");
            AddPrimaryKey("dbo.ProductColor", "Id");
            CreateIndex("dbo.ProductColor", "ProductId");
            AddForeignKey("dbo.ProductColor", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
    }
}
