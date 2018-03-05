namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductFollowerGroupAndProductFileFollowerGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductFileFollowerGroups", "CompanyFollowerGroupID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.ProductFileFollowerGroups", "ProductFileID", "dbo.ProductFiles");
            DropForeignKey("dbo.ProductFollowerGroups", "CompanyFollowerGroupID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.ProductFollowerGroups", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductFileFollowerGroups", new[] { "CompanyFollowerGroupID" });
            DropIndex("dbo.ProductFileFollowerGroups", new[] { "ProductFileID" });
            DropIndex("dbo.ProductFollowerGroups", new[] { "CompanyFollowerGroupID" });
            DropIndex("dbo.ProductFollowerGroups", new[] { "ProductID" });
            DropTable("dbo.ProductFileFollowerGroups");
            DropTable("dbo.ProductFollowerGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductFollowerGroups",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CompanyFollowerGroupID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductFileFollowerGroups",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CompanyFollowerGroupID = c.Long(nullable: false),
                        ProductFileID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.ProductFollowerGroups", "ProductID");
            CreateIndex("dbo.ProductFollowerGroups", "CompanyFollowerGroupID");
            CreateIndex("dbo.ProductFileFollowerGroups", "ProductFileID");
            CreateIndex("dbo.ProductFileFollowerGroups", "CompanyFollowerGroupID");
            AddForeignKey("dbo.ProductFollowerGroups", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductFollowerGroups", "CompanyFollowerGroupID", "dbo.CompanyFollowerGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductFileFollowerGroups", "ProductFileID", "dbo.ProductFiles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ProductFileFollowerGroups", "CompanyFollowerGroupID", "dbo.CompanyFollowerGroups", "ID", cascadeDelete: true);
        }
    }
}
