namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivitiesAndUpdateUserLoginDates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        Verb = c.Int(nullable: false),
                        TargetEntityID = c.Long(nullable: false),
                        ContextEntityID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            AddColumn("dbo.AspNetUsers", "LastLoginDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "CurrentLoginDate", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "LastLogin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime());
            DropForeignKey("dbo.Activities", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Activities", new[] { "UserID" });
            DropColumn("dbo.AspNetUsers", "CurrentLoginDate");
            DropColumn("dbo.AspNetUsers", "LastLoginDate");
            DropTable("dbo.Activities");
        }
    }
}
