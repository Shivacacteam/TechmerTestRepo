namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GridImplementsAsset : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Grid");
            AddColumn("dbo.Grid", "zIndexTimeStamp", c => c.Long(nullable: false));
            RenameColumn("dbo.Grid", "Id", "IdOld");
            AddColumn("dbo.Grid", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            DropColumn("dbo.Grid", "IdOld");
            AddPrimaryKey("dbo.Grid", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Grid");
            RenameColumn("dbo.Grid", "Id", "IdOld");
            AddColumn("dbo.Grid", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Grid", "IdOld");
            DropColumn("dbo.Grid", "zIndexTimeStamp");
            AddPrimaryKey("dbo.Grid", "Id");
        }
    }
}
