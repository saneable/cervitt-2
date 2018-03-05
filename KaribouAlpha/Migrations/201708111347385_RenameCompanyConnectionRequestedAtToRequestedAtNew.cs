namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCompanyConnectionRequestedAtToRequestedAtNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyConnections", "RequestedAtNew", c => c.DateTime(nullable: false));
            DropColumn("dbo.CompanyConnections", "RequestedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompanyConnections", "RequestedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.CompanyConnections", "RequestedAtNew");
        }
    }
}
