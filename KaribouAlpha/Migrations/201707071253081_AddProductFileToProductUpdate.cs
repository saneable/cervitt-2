namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductFileToProductUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ProductFileID1", c => c.Long());
            CreateIndex("dbo.Articles", "ProductFileID1");
            AddForeignKey("dbo.Articles", "ProductFileID1", "dbo.ProductFiles", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "ProductFileID1", "dbo.ProductFiles");
            DropIndex("dbo.Articles", new[] { "ProductFileID1" });
            DropColumn("dbo.Articles", "ProductFileID1");
        }
    }
}
