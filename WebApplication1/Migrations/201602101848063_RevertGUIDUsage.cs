namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertGUIDUsage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.ProductColor", "ProductId", "dbo.Product");
            DropIndex("dbo.ColorSelection", new[] { "WorkspaceId" });
            DropIndex("dbo.Grid", new[] { "WorkspaceId" });
            DropIndex("dbo.ProductColor", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "WorkspaceId" });
            DropPrimaryKey("dbo.ColorSelection");
            DropPrimaryKey("dbo.Workspace");
            DropPrimaryKey("dbo.Grid");
            DropPrimaryKey("dbo.ProductColor");
            DropPrimaryKey("dbo.Product");

            RenameColumn("dbo.ColorSelection", "Id", "IdOld");
            RenameColumn("dbo.ColorSelection", "WorkspaceId", "WorkspaceIdOld");
            RenameColumn("dbo.Workspace", "Id", "IdOld");
            RenameColumn("dbo.Grid", "Id", "IdOld");
            RenameColumn("dbo.Grid", "WorkspaceId", "WorkspaceIdOld");
            RenameColumn("dbo.ProductColor", "Id", "IdOld");
            RenameColumn("dbo.ProductColor", "ProductId", "ProductIdOld");
            RenameColumn("dbo.Product", "Id", "IdOld");
            RenameColumn("dbo.Product", "WorkspaceId", "WorkspaceIdOld");

            AddColumn("dbo.ColorSelection", "Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ColorSelection", "WorkspaceId", c => c.Long(nullable: false));
            AddColumn("dbo.Workspace", "Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Grid", "Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Grid", "WorkspaceId", c => c.Long(nullable: false));
            AddColumn("dbo.ProductColor", "Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.ProductColor", "ProductId", c => c.Long(nullable: false));
            AddColumn("dbo.Product", "Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Product", "WorkspaceId", c => c.Long(nullable: false));

            Sql(@"
                UPDATE Product
                SET Product.WorkspaceId = Workspace.Id
                FROM Product
                    Inner Join Workspace
                        On Product.WorkspaceIdOld = Workspace.IdOld
            ");
            Sql(@"
                UPDATE Grid
                SET Grid.WorkspaceId = Workspace.Id
                FROM Grid
                    Inner Join Workspace
                        On Grid.WorkspaceIdOld = Workspace.IdOld
            ");
            Sql(@"
                UPDATE ColorSelection
                SET ColorSelection.WorkspaceId = Workspace.Id
                FROM ColorSelection
                    Inner Join Workspace
                        On ColorSelection.WorkspaceIdOld = Workspace.IdOld
            ");
            Sql(@"
                UPDATE dbo.ProductColor
                SET ProductColor.ProductId = Product.Id
                FROM ProductColor
                    Inner Join Product
                        On ProductColor.ProductIdOld = Product.IdOld
            ");

            DropColumn("dbo.ColorSelection", "IdOld");
            DropColumn("dbo.ColorSelection", "WorkspaceIdOld");
            DropColumn("dbo.Workspace", "IdOld");
            DropColumn("dbo.Grid", "IdOld");
            DropColumn("dbo.Grid", "WorkspaceIdOld");
            DropColumn("dbo.ProductColor", "IdOld");
            DropColumn("dbo.ProductColor", "ProductIdOld");
            DropColumn("dbo.Product", "IdOld");
            DropColumn("dbo.Product", "WorkspaceIdOld");


            AddPrimaryKey("dbo.ColorSelection", "Id");
            AddPrimaryKey("dbo.Workspace", "Id");
            AddPrimaryKey("dbo.Grid", "Id");
            AddPrimaryKey("dbo.ProductColor", "Id");
            AddPrimaryKey("dbo.Product", "Id");
            CreateIndex("dbo.ColorSelection", "WorkspaceId");
            CreateIndex("dbo.Grid", "WorkspaceId");
            CreateIndex("dbo.ProductColor", "ProductId");
            CreateIndex("dbo.Product", "WorkspaceId");
            AddForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductColor", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColor", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace");
            DropIndex("dbo.Product", new[] { "WorkspaceId" });
            DropIndex("dbo.ProductColor", new[] { "ProductId" });
            DropIndex("dbo.Grid", new[] { "WorkspaceId" });
            DropIndex("dbo.ColorSelection", new[] { "WorkspaceId" });
            DropPrimaryKey("dbo.Product");
            DropPrimaryKey("dbo.ProductColor");
            DropPrimaryKey("dbo.Grid");
            DropPrimaryKey("dbo.Workspace");
            DropPrimaryKey("dbo.ColorSelection");

            RenameColumn("dbo.ColorSelection", "Id", "IdOld");
            RenameColumn("dbo.ColorSelection", "WorkspaceId", "WorkspaceIdOld");
            RenameColumn("dbo.Workspace", "Id", "IdOld");
            RenameColumn("dbo.Grid", "Id", "IdOld");
            RenameColumn("dbo.Grid", "WorkspaceId", "WorkspaceIdOld");
            RenameColumn("dbo.ProductColor", "Id", "IdOld");
            RenameColumn("dbo.ProductColor", "ProductId", "ProductIdOld");
            RenameColumn("dbo.Product", "Id", "IdOld");
            RenameColumn("dbo.Product", "WorkspaceId", "WorkspaceIdOld");

            AddColumn("dbo.Product", "WorkspaceId", c => c.Guid(nullable: false));
            AddColumn("dbo.Product", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddColumn("dbo.ProductColor", "ProductId", c => c.Guid(nullable: false));
            AddColumn("dbo.ProductColor", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddColumn("dbo.Grid", "WorkspaceId", c => c.Guid(nullable: false));
            AddColumn("dbo.Grid", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddColumn("dbo.Workspace", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddColumn("dbo.ColorSelection", "WorkspaceId", c => c.Guid(nullable: false));
            AddColumn("dbo.ColorSelection", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));

            Sql(@"
                UPDATE Product
                SET Product.WorkspaceId = Workspace.Id
                FROM Product
                    Inner Join Workspace
                        On Product.WorkspaceIdOld = Workspace.IdOld
            ");
            Sql(@"
                UPDATE Grid
                SET Grid.WorkspaceId = Workspace.Id
                FROM Grid
                    Inner Join Workspace
                        On Grid.WorkspaceIdOld = Workspace.IdOld
            ");
            Sql(@"
                UPDATE ColorSelection
                SET ColorSelection.WorkspaceId = Workspace.Id
                FROM ColorSelection
                    Inner Join Workspace
                        On ColorSelection.WorkspaceIdOld = Workspace.IdOld
            ");
            Sql(@"
                UPDATE dbo.ProductColor
                SET ProductColor.ProductId = Product.Id
                FROM ProductColor
                    Inner Join Product
                        On ProductColor.ProductIdOld = Product.IdOld
            ");

            DropColumn("dbo.ColorSelection", "IdOld");
            DropColumn("dbo.ColorSelection", "WorkspaceIdOld");
            DropColumn("dbo.Workspace", "IdOld");
            DropColumn("dbo.Grid", "IdOld");
            DropColumn("dbo.Grid", "WorkspaceIdOld");
            DropColumn("dbo.ProductColor", "IdOld");
            DropColumn("dbo.ProductColor", "ProductIdOld");
            DropColumn("dbo.Product", "IdOld");
            DropColumn("dbo.Product", "WorkspaceIdOld");

            AddPrimaryKey("dbo.Product", "Id");
            AddPrimaryKey("dbo.ProductColor", "Id");
            AddPrimaryKey("dbo.Grid", "Id");
            AddPrimaryKey("dbo.Workspace", "Id");
            AddPrimaryKey("dbo.ColorSelection", "Id");
            CreateIndex("dbo.Product", "WorkspaceId");
            CreateIndex("dbo.ProductColor", "ProductId");
            CreateIndex("dbo.Grid", "WorkspaceId");
            CreateIndex("dbo.ColorSelection", "WorkspaceId");
            AddForeignKey("dbo.ProductColor", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
        }
    }
}
