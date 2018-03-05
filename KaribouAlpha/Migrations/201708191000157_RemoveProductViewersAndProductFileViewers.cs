namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductViewersAndProductFileViewers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductViewers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductViewers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductFileViewers", "ProductFileID", "dbo.ProductFiles");
            DropForeignKey("dbo.ProductFileViewers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.ProductViewers", new[] { "UserID" });
            DropIndex("dbo.ProductViewers", new[] { "ProductID" });
            DropIndex("dbo.ProductFileViewers", new[] { "UserID" });
            DropIndex("dbo.ProductFileViewers", new[] { "ProductFileID" });
            DropTable("dbo.ProductViewers");
            DropTable("dbo.ProductFileViewers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductFileViewers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ProductFileID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductViewers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.ProductFileViewers", "ProductFileID");
            CreateIndex("dbo.ProductFileViewers", "UserID");
            CreateIndex("dbo.ProductViewers", "ProductID");
            CreateIndex("dbo.ProductViewers", "UserID");
            AddForeignKey("dbo.ProductFileViewers", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductFileViewers", "ProductFileID", "dbo.ProductFiles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductViewers", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductViewers", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
        }
    }
}
