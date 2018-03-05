namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveProductUpdatesOutOfTheArticleHierarchy : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Articles", new[] { "ProductFileID1" });
            CreateTable(
                "dbo.ProductUpdates",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        ProductFileID = c.Long(nullable: false),
                        UpdateType = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserID)
                .Index(t => t.ProductID)
                .Index(t => t.ProductFileID);
            
            AddForeignKey("dbo.ProductUpdates", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ProductUpdates", "ProductID", "dbo.Products", "ID", cascadeDelete: false);
            AddForeignKey("dbo.ProductUpdates", "ProductFileID", "dbo.ProductFiles", "ID", cascadeDelete: false);
            DropForeignKey("dbo.Articles", "ProductFileID1", "dbo.ProductFiles");
            DropColumn("dbo.Articles", "ProductFileID1");
            DropColumn("dbo.Articles", "UpdateType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "UpdateType", c => c.Int());
            AddColumn("dbo.Articles", "ProductFileID1", c => c.Long());
            DropForeignKey("dbo.Articles", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductUpdates", new[] { "ProductFileID" });
            DropIndex("dbo.ProductUpdates", new[] { "ProductID" });
            DropIndex("dbo.ProductUpdates", new[] { "UserID" });
            DropTable("dbo.ProductUpdates");
            RenameColumn(table: "dbo.ProductUpdates", name: "ProductFileID", newName: "ProductFileID1");
            CreateIndex("dbo.Articles", "ProductFileID1");
        }
    }
}
