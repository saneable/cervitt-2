namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsItemFeedbackAddProductFileDownloadAndRenameFeedbackToProductFileFeedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFileDownloads",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductFileID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductFiles", t => t.ProductFileID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProductFileID)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Articles", "NewsItemID", c => c.Long());
            CreateIndex("dbo.Articles", "NewsItemID");
            AddForeignKey("dbo.Articles", "NewsItemID", "dbo.Articles", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "NewsItemID", "dbo.Articles");
            DropForeignKey("dbo.ProductFileDownloads", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductFileDownloads", "ProductFileID", "dbo.ProductFiles");
            DropIndex("dbo.ProductFileDownloads", new[] { "UserID" });
            DropIndex("dbo.ProductFileDownloads", new[] { "ProductFileID" });
            DropIndex("dbo.Articles", new[] { "NewsItemID" });
            DropColumn("dbo.Articles", "NewsItemID");
            DropTable("dbo.ProductFileDownloads");
        }
    }
}
