namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeOptionalProductInNewsItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "ProductID", "dbo.Products");
            AddForeignKey("dbo.Articles", "ProductID", "dbo.Products", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "ProductID", "dbo.Products");
            AddForeignKey("dbo.Articles", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
        }
    }
}
