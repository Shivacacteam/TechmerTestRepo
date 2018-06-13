namespace TechmerVision.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A sample inspiration file name. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class SampleInspirationFileName : DbMigration
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the upgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Up()
        {
            AddColumn("dbo.SampleInspiration", "Filename", c => c.String());
            Sql("Update SampleInspiration Set Filename = Id");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operations to be performed during the downgrade process. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void Down()
        {
            DropColumn("dbo.SampleInspiration", "Filename");
        }
    }
}
