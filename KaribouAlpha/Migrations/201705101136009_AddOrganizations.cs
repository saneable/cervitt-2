namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        DisplayName = c.String(),
                        TradingName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        WebsiteUrl = c.String(),
                        Sector = c.String(),
                        Country = c.String(),
                        LinkedInUrl = c.String(),
                        TwitterUrl = c.String(),
                        FacebookUrl = c.String(),
                        InstagramUrl = c.String(),
                        PinterestUrl = c.String(),
                        TumblrUrl = c.String(),
                        ConnectionRequests = c.Int(nullable: false),
                        ConnectionPersonalSuggestions = c.Int(nullable: false),
                        ConnectionCompanySuggestions = c.Int(nullable: false),
                        ConnectionUpdates = c.Int(nullable: false),
                        Feedback = c.Int(nullable: false),
                        CompanyPageStats = c.Int(nullable: false),
                        CervittNewsAndTips = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organizations", "ID", "dbo.AspNetUsers");
            DropIndex("dbo.Organizations", new[] { "ID" });
            DropTable("dbo.Organizations");
        }
    }
}
