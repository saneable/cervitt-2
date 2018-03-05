namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsAndConnections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyConnections",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        CompanyID = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.CompanyMembers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        CompanyID = c.Long(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.PersonalConnections",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FollowingID = c.Long(nullable: false),
                        FollowerID = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowingID, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerID, cascadeDelete: false)
                .Index(t => t.FollowingID)
                .Index(t => t.FollowerID);
            
            CreateTable(
                "dbo.ProductTeamMembers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        Role = c.String(),
                        CanEditTheProduct = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CompanyID = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Privacy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                        Bytes = c.Binary(nullable: false),
                        UploadedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTeamMembers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductTeamMembers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductFiles", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.PersonalConnections", "FollowerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PersonalConnections", "FollowingID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyMembers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyMembers", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.CompanyConnections", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CompanyConnections", "CompanyID", "dbo.Companies");
            DropIndex("dbo.ProductFiles", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "CompanyID" });
            DropIndex("dbo.ProductTeamMembers", new[] { "UserID" });
            DropIndex("dbo.ProductTeamMembers", new[] { "ProductID" });
            DropIndex("dbo.PersonalConnections", new[] { "FollowerID" });
            DropIndex("dbo.PersonalConnections", new[] { "FollowingID" });
            DropIndex("dbo.CompanyMembers", new[] { "CompanyID" });
            DropIndex("dbo.CompanyMembers", new[] { "UserID" });
            DropIndex("dbo.CompanyConnections", new[] { "CompanyID" });
            DropIndex("dbo.CompanyConnections", new[] { "UserID" });
            DropTable("dbo.ProductFiles");
            DropTable("dbo.Products");
            DropTable("dbo.ProductTeamMembers");
            DropTable("dbo.PersonalConnections");
            DropTable("dbo.CompanyMembers");
            DropTable("dbo.CompanyConnections");
        }
    }
}
