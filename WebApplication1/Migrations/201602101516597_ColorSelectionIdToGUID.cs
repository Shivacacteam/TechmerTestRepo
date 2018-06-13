namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColorSelectionIdToGUID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ColorSelection");
            RenameColumn("dbo.ColorSelection", "Id", "IdOld");
            AddColumn("dbo.ColorSelection", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            DropColumn("dbo.ColorSelection", "IdOld");
            AddPrimaryKey("dbo.ColorSelection", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ColorSelection");
            RenameColumn("dbo.ColorSelection", "Id", "IdOld");
            AddColumn("dbo.ColorSelection", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.ColorSelection", "IdOld");
            AddPrimaryKey("dbo.ColorSelection", "Id");
        }
    }
}
