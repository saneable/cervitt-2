namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeOptionalProductFileInProductUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductUpdates", "ProductFileID", "dbo.ProductFiles");
            DropIndex("dbo.ProductUpdates", new[] { "ProductFileID" });
            AlterColumn("dbo.ProductUpdates", "ProductFileID", c => c.Long());
            CreateIndex("dbo.ProductUpdates", "ProductFileID");
            AddForeignKey("dbo.ProductUpdates", "ProductFileID", "dbo.ProductFiles", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductUpdates", "ProductFileID", "dbo.ProductFiles");
            DropIndex("dbo.ProductUpdates", new[] { "ProductFileID" });
            AlterColumn("dbo.ProductUpdates", "ProductFileID", c => c.Long(nullable: false));
            CreateIndex("dbo.ProductUpdates", "ProductFileID");
            AddForeignKey("dbo.ProductUpdates", "ProductFileID", "dbo.ProductFiles", "ID", cascadeDelete: true);
        }
    }
}
