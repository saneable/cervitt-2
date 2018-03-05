namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcceptedAtToCompanyConnection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyConnections", "AcceptedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyConnections", "AcceptedAt");
        }
    }
}
