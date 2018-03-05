namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociateCompaniesToUsers : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Companies");
            AddColumn("dbo.Companies", "LinkedInUrl", c => c.String());
            AddColumn("dbo.Companies", "TwitterUrl", c => c.String());
            AddColumn("dbo.Companies", "FacebookUrl", c => c.String());
            AddColumn("dbo.Companies", "InstagramUrl", c => c.String());
            AddColumn("dbo.Companies", "PinterestUrl", c => c.String());
            AddColumn("dbo.Companies", "TumblrUrl", c => c.String());
            AddColumn("dbo.Companies", "ConnectionRequests", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "ConnectionPersonalSuggestions", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "ConnectionCompanySuggestions", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "ConnectionUpdates", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "Feedback", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "CompanyPageStats", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "CervittNewsAndTips", c => c.Int(nullable: false));
            AddColumn("dbo.Companies", "Image", c => c.Binary());
            AlterColumn("dbo.Companies", "ID", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Companies", "ID");
            CreateIndex("dbo.Companies", "ID");
            AddForeignKey("dbo.Companies", "ID", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "Company");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Company", c => c.String());
            DropForeignKey("dbo.Companies", "ID", "dbo.AspNetUsers");
            DropIndex("dbo.Companies", new[] { "ID" });
            DropPrimaryKey("dbo.Companies");
            AlterColumn("dbo.Companies", "ID", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Companies", "Image");
            DropColumn("dbo.Companies", "CervittNewsAndTips");
            DropColumn("dbo.Companies", "CompanyPageStats");
            DropColumn("dbo.Companies", "Feedback");
            DropColumn("dbo.Companies", "ConnectionUpdates");
            DropColumn("dbo.Companies", "ConnectionCompanySuggestions");
            DropColumn("dbo.Companies", "ConnectionPersonalSuggestions");
            DropColumn("dbo.Companies", "ConnectionRequests");
            DropColumn("dbo.Companies", "TumblrUrl");
            DropColumn("dbo.Companies", "PinterestUrl");
            DropColumn("dbo.Companies", "InstagramUrl");
            DropColumn("dbo.Companies", "FacebookUrl");
            DropColumn("dbo.Companies", "TwitterUrl");
            DropColumn("dbo.Companies", "LinkedInUrl");
            AddPrimaryKey("dbo.Companies", "ID");
        }
    }
}
