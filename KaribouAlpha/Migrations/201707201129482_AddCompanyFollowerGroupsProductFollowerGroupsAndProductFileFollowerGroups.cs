namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyFollowerGroupsProductFollowerGroupsAndProductFileFollowerGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyFollowerGroups",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.ProductFileFollowerGroups",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CompanyFollowerGroupID = c.Long(nullable: false),
                        ProductFileID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CompanyFollowerGroups", t => t.CompanyFollowerGroupID, cascadeDelete: true)
                .ForeignKey("dbo.ProductFiles", t => t.ProductFileID, cascadeDelete: false)
                .Index(t => t.CompanyFollowerGroupID)
                .Index(t => t.ProductFileID);
            
            CreateTable(
                "dbo.ProductFollowerGroups",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CompanyFollowerGroupID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CompanyFollowerGroups", t => t.CompanyFollowerGroupID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: false)
                .Index(t => t.CompanyFollowerGroupID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.CompanyConnectionCompanyFollowerGroups",
                c => new
                    {
                        CompanyConnection_ID = c.Long(nullable: false),
                        CompanyFollowerGroup_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompanyConnection_ID, t.CompanyFollowerGroup_ID })
                .ForeignKey("dbo.CompanyConnections", t => t.CompanyConnection_ID, cascadeDelete: true)
                .ForeignKey("dbo.CompanyFollowerGroups", t => t.CompanyFollowerGroup_ID, cascadeDelete: false)
                .Index(t => t.CompanyConnection_ID)
                .Index(t => t.CompanyFollowerGroup_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFollowerGroups", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductFollowerGroups", "CompanyFollowerGroupID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.ProductFileFollowerGroups", "ProductFileID", "dbo.ProductFiles");
            DropForeignKey("dbo.ProductFileFollowerGroups", "CompanyFollowerGroupID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.CompanyConnectionCompanyFollowerGroups", "CompanyFollowerGroup_ID", "dbo.CompanyFollowerGroups");
            DropForeignKey("dbo.CompanyConnectionCompanyFollowerGroups", "CompanyConnection_ID", "dbo.CompanyConnections");
            DropForeignKey("dbo.CompanyFollowerGroups", "CompanyID", "dbo.Companies");
            DropIndex("dbo.CompanyConnectionCompanyFollowerGroups", new[] { "CompanyFollowerGroup_ID" });
            DropIndex("dbo.CompanyConnectionCompanyFollowerGroups", new[] { "CompanyConnection_ID" });
            DropIndex("dbo.ProductFollowerGroups", new[] { "ProductID" });
            DropIndex("dbo.ProductFollowerGroups", new[] { "CompanyFollowerGroupID" });
            DropIndex("dbo.ProductFileFollowerGroups", new[] { "ProductFileID" });
            DropIndex("dbo.ProductFileFollowerGroups", new[] { "CompanyFollowerGroupID" });
            DropIndex("dbo.CompanyFollowerGroups", new[] { "CompanyID" });
            DropTable("dbo.CompanyConnectionCompanyFollowerGroups");
            DropTable("dbo.ProductFollowerGroups");
            DropTable("dbo.ProductFileFollowerGroups");
            DropTable("dbo.CompanyFollowerGroups");
        }
    }
}
