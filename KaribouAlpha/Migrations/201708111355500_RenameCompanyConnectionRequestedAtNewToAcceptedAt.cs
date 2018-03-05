namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCompanyConnectionRequestedAtNewToAcceptedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyConnections", "AcceptedAt", c => c.DateTime());
            DropColumn("dbo.CompanyConnections", "RequestedAtNew");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompanyConnections", "RequestedAtNew", c => c.DateTime(nullable: false));
            DropColumn("dbo.CompanyConnections", "AcceptedAt");
        }
    }
}
