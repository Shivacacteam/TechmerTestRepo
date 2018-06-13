#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace TechmerVision.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;


    public partial class InvitationCreation : DbMigration

    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        email = c.String(),
                        password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(),
                        Title = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        DesignRole = c.String(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Invitations");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member