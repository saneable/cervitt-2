namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        Title = c.String(nullable: false),
                        Body = c.String(),
                        LinkUrl = c.String(),
                        ProductFileID = c.Long(),
                        ReplyToID = c.Long(),
                        Visibility = c.Int(),
                        ProductID = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.ProductFiles", t => t.ProductFileID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: false)
                .ForeignKey("dbo.Articles", t => t.ReplyToID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductFileID)
                .Index(t => t.ReplyToID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "ReplyToID", "dbo.Articles");
            DropForeignKey("dbo.Articles", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Articles", "ProductFileID", "dbo.ProductFiles");
            DropForeignKey("dbo.Articles", "UserID", "dbo.Companies");
            DropIndex("dbo.Articles", new[] { "ProductID" });
            DropIndex("dbo.Articles", new[] { "ReplyToID" });
            DropIndex("dbo.Articles", new[] { "ProductFileID" });
            DropIndex("dbo.Articles", new[] { "UserID" });
            DropTable("dbo.Articles");
        }
    }
}
