namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A product color filename. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class ProductColorFilename : DbMigration
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the upgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Up()
        {
            AddColumn("dbo.ProductColor", "Filename", c => c.String());
            Sql("Update ProductColor Set Filename = Id");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the downgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Down()
        {
            DropColumn("dbo.ProductColor", "Filename");
        }
    }
}
