namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogoToCompanyProductViewersAndProductFileViewers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFileViewers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ProductFileID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductFiles", t => t.ProductFileID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductFileID);
            
            CreateTable(
                "dbo.ProductViewers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductID);
            
            AddColumn("dbo.Companies", "Logo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductViewers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductViewers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductFileViewers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductFileViewers", "ProductFileID", "dbo.ProductFiles");
            DropIndex("dbo.ProductViewers", new[] { "ProductID" });
            DropIndex("dbo.ProductViewers", new[] { "UserID" });
            DropIndex("dbo.ProductFileViewers", new[] { "ProductFileID" });
            DropIndex("dbo.ProductFileViewers", new[] { "UserID" });
            DropColumn("dbo.Companies", "Logo");
            DropTable("dbo.ProductViewers");
            DropTable("dbo.ProductFileViewers");
        }
    }
}
