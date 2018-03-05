namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCompanies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "ID", "dbo.AspNetUsers");
            DropIndex("dbo.Companies", new[] { "ID" });
            DropTable("dbo.Companies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Companies",
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Companies", "ID");
            AddForeignKey("dbo.Companies", "ID", "dbo.AspNetUsers", "Id");
        }
    }
}
