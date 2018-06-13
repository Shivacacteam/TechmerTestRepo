namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkspaceImplementsAsset : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace");
            DropIndex("dbo.ColorSelection", new[] { "WorkspaceId" });
            DropIndex("dbo.Grid", new[] { "WorkspaceId" });
            DropIndex("dbo.Product", new[] { "WorkspaceId" });
            DropPrimaryKey("dbo.Workspace");

            AddColumn("dbo.Workspace", "ModifiedTimeStamp", c => c.Long(nullable: false));
            AddColumn("dbo.Workspace", "zIndexTimeStamp", c => c.Long(nullable: false));

            RenameColumn("dbo.ColorSelection", "WorkspaceId", "WorkspaceIdOld");
            AddColumn("dbo.ColorSelection", "WorkspaceId", c => c.Guid(nullable: false));

            RenameColumn("dbo.Grid", "WorkspaceId", "WorkspaceIdOld");
            AddColumn("dbo.Grid", "WorkspaceId", c => c.Guid(nullable: false));

            RenameColumn("dbo.Product", "WorkspaceId", "WorkspaceIdOld");
            AddColumn("dbo.Product", "WorkspaceId", c => c.Guid(nullable: false));

            RenameColumn("dbo.Workspace", "Id", "IdOld");
            AddColumn("dbo.Workspace", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
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

            DropColumn("dbo.ColorSelection", "WorkspaceIdOld");
            DropColumn("dbo.Grid", "WorkspaceIdOld");
            DropColumn("dbo.Product", "WorkspaceIdOld");
            DropColumn("dbo.Workspace", "IdOld");

            AddPrimaryKey("dbo.Workspace", "Id");
            CreateIndex("dbo.ColorSelection", "WorkspaceId");
            CreateIndex("dbo.Grid", "WorkspaceId");
            CreateIndex("dbo.Product", "WorkspaceId");
            AddForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace");
            DropForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace");
            DropIndex("dbo.Product", new[] { "WorkspaceId" });
            DropIndex("dbo.Grid", new[] { "WorkspaceId" });
            DropIndex("dbo.ColorSelection", new[] { "WorkspaceId" });
            DropPrimaryKey("dbo.Workspace");

            RenameColumn("dbo.Product", "WorkspaceId", "WorkspaceIdOld");
            AddColumn("dbo.Product", "WorkspaceId", c => c.Long(nullable: false));

            RenameColumn("dbo.Grid", "WorkspaceId", "WorkspaceIdOld");
            AddColumn("dbo.Grid", "WorkspaceId", c => c.Long(nullable: false));
            
            RenameColumn("dbo.ColorSelection", "WorkspaceId", "WorkspaceIdOld");
            AddColumn("dbo.ColorSelection", "WorkspaceId", c => c.Long(nullable: false));

            RenameColumn("dbo.Workspace", "Id", "OldId");
            AddColumn("dbo.Workspace", "Id", c => c.Long(nullable: false, identity: true));

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

            DropColumn("dbo.ColorSelection", "WorkspaceIdOld");
            DropColumn("dbo.Grid", "WorkspaceIdOld");
            DropColumn("dbo.Product", "WorkspaceIdOld");
            DropColumn("dbo.Workspace", "IdOld");
            DropColumn("dbo.Workspace", "zIndexTimeStamp");
            DropColumn("dbo.Workspace", "ModifiedTimeStamp");


            AddPrimaryKey("dbo.Workspace", "Id");
            CreateIndex("dbo.Product", "WorkspaceId");
            CreateIndex("dbo.Grid", "WorkspaceId");
            CreateIndex("dbo.ColorSelection", "WorkspaceId");
            AddForeignKey("dbo.Product", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Grid", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ColorSelection", "WorkspaceId", "dbo.Workspace", "Id", cascadeDelete: true);
        }
    }
}
