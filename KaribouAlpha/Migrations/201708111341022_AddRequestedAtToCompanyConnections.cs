namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestedAtToCompanyConnections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastCheckForConnectionRequests", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastCheckForProductUpdates", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastCheckForFeedback", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LastCheckForNewsItems", c => c.DateTime());
            AddColumn("dbo.CompanyConnections", "RequestedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyConnections", "RequestedAt");
            DropColumn("dbo.AspNetUsers", "LastCheckForNewsItems");
            DropColumn("dbo.AspNetUsers", "LastCheckForFeedback");
            DropColumn("dbo.AspNetUsers", "LastCheckForProductUpdates");
            DropColumn("dbo.AspNetUsers", "LastCheckForConnectionRequests");
        }
    }
}
