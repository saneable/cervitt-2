namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Town", c => c.String());
            AddColumn("dbo.AspNetUsers", "Company", c => c.String());
            AddColumn("dbo.AspNetUsers", "JobTitle", c => c.String());
            AddColumn("dbo.AspNetUsers", "FacebooknUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "InstagramUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "PinterestUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "TumblrUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "ConnectionRequests", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ConnectionPersonalSuggestions", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ConnectionCompanySuggestions", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ConnectionUpdates", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Feedback", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CompanyPageStats", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CervittNewsAndTips", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CervittNewsAndTips");
            DropColumn("dbo.AspNetUsers", "CompanyPageStats");
            DropColumn("dbo.AspNetUsers", "Feedback");
            DropColumn("dbo.AspNetUsers", "ConnectionUpdates");
            DropColumn("dbo.AspNetUsers", "ConnectionCompanySuggestions");
            DropColumn("dbo.AspNetUsers", "ConnectionPersonalSuggestions");
            DropColumn("dbo.AspNetUsers", "ConnectionRequests");
            DropColumn("dbo.AspNetUsers", "TumblrUrl");
            DropColumn("dbo.AspNetUsers", "PinterestUrl");
            DropColumn("dbo.AspNetUsers", "InstagramUrl");
            DropColumn("dbo.AspNetUsers", "FacebooknUrl");
            DropColumn("dbo.AspNetUsers", "JobTitle");
            DropColumn("dbo.AspNetUsers", "Company");
            DropColumn("dbo.AspNetUsers", "Town");
        }
    }
}
