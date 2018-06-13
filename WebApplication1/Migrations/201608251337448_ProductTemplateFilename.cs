namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A product template filename. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class ProductTemplateFilename : DbMigration
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the upgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Up()
        {
            AddColumn("dbo.ProductTemplate", "FileName", c => c.String());
            Sql("Update ProductTemplate Set Filename = Id");
            AddColumn("dbo.ProductTemplateColor", "FileName", c => c.String());
            Sql("Update ProductTemplateColor Set Filename = Id");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the downgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Down()
        {
            DropColumn("dbo.ProductTemplateColor", "FileName");
            DropColumn("dbo.ProductTemplate", "FileName");
        }
    }
}
