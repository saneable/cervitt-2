namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCompanyConnectionAcceptedAtToRequestedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyConnections", "RequestedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.CompanyConnections", "AcceptedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompanyConnections", "AcceptedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.CompanyConnections", "RequestedAt");
        }
    }
}
