namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductViews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductViews",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductViews", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductViews", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductViews", new[] { "UserID" });
            DropIndex("dbo.ProductViews", new[] { "ProductID" });
            DropTable("dbo.ProductViews");
        }
    }
}
