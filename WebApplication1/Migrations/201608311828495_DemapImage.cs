namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A demap image. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class DemapImage : DbMigration
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the upgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Up()
        {
            DropColumn("dbo.Workspace", "Image");
            DropColumn("dbo.Grid", "Image");
            DropColumn("dbo.ProductColor", "Image");
            DropColumn("dbo.Product", "Image");
            DropColumn("dbo.ProductTemplate", "Image");
            DropColumn("dbo.ProductTemplateColor", "Image");
            DropColumn("dbo.SampleInspiration", "Image");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the downgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Down()
        {
            AddColumn("dbo.SampleInspiration", "Image", c => c.String());
            AddColumn("dbo.ProductTemplateColor", "Image", c => c.String());
            AddColumn("dbo.ProductTemplate", "Image", c => c.String());
            AddColumn("dbo.Product", "Image", c => c.String());
            AddColumn("dbo.ProductColor", "Image", c => c.String());
            AddColumn("dbo.Grid", "Image", c => c.String());
            AddColumn("dbo.Workspace", "Image", c => c.String());
        }
    }
}
