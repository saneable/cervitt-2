namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyRelationshipsBetweenCompanyFollowerGroupsProductsAndProductFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFileCompanyFollowerGroups",
                c => new
                    {
                        ProductFile_ID = c.Long(nullable: false),
                        CompanyFollowerGroup_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductFile_ID, t.CompanyFollowerGroup_ID })
                .ForeignKey("dbo.ProductFiles", t => t.ProductFile_ID, cascadeDelete: true)
                .ForeignKey("dbo.CompanyFollowerGroups", t => t.CompanyFollowerGroup_ID, cascadeDelete: false)
                .Index(t => t.ProductFile_ID)
                .Index(t => t.CompanyFollowerGroup_ID);
            
            CreateTable(
                "dbo.ProductCompanyFollowerGroups",
                c => new
                    {
                        Product_ID = c.Long(nullable: false),
                        CompanyFollowerGroup_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ID, t.CompanyFollowerGroup_ID })
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.CompanyFollowerGroups", t => t.CompanyFollowerGroup_ID, cascadeDelete: false)
                .Index(t => t.Product_ID)
                .Index(t => t.CompanyFollowerGroup_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCompanyFollowerGroups", "CompanyFollowerGroup_ID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.ProductCompanyFollowerGroups", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.ProductFileCompanyFollowerGroups", "CompanyFollowerGroup_ID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.ProductFileCompanyFollowerGroups", "ProductFile_ID", "dbo.ProductFiles");
            DropIndex("dbo.ProductCompanyFollowerGroups", new[] { "CompanyFollowerGroup_ID" });
            DropIndex("dbo.ProductCompanyFollowerGroups", new[] { "Product_ID" });
            DropIndex("dbo.ProductFileCompanyFollowerGroups", new[] { "CompanyFollowerGroup_ID" });
            DropIndex("dbo.ProductFileCompanyFollowerGroups", new[] { "ProductFile_ID" });
            DropTable("dbo.ProductCompanyFollowerGroups");
            DropTable("dbo.ProductFileCompanyFollowerGroups");
        }
    }
}
