namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.Entity.Migrations.Model;

    public partial class ChangeAssetDataTypes : DbMigration
    {

        public override void Up()
        {   
            AlterColumn("dbo.Workspace", "x", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Workspace", "y", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Workspace", "s", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue:1));
            AlterColumn("dbo.Workspace", "r", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Grid", "x", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Grid", "y", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Grid", "s", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 1));
            AlterColumn("dbo.Grid", "r", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "x", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "y", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Product", "s", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 1));
            AlterColumn("dbo.Product", "r", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "r", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "s", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "y", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "x", c => c.Int(nullable: false));
            AlterColumn("dbo.Grid", "r", c => c.Int(nullable: false));
            AlterColumn("dbo.Grid", "s", c => c.Int(nullable: false));
            AlterColumn("dbo.Grid", "y", c => c.Int(nullable: false));
            AlterColumn("dbo.Grid", "x", c => c.Int(nullable: false));
            AlterColumn("dbo.Workspace", "r", c => c.Int(nullable: false));
            AlterColumn("dbo.Workspace", "s", c => c.Int(nullable: false));
            AlterColumn("dbo.Workspace", "y", c => c.Int(nullable: false));
            AlterColumn("dbo.Workspace", "x", c => c.Int(nullable: false));
        }
    }
}
