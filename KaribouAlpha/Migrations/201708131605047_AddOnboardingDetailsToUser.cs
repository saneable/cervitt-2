namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOnboardingDetailsToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OnboardingSkipped", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "OnboardingStep", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "OnboardingStep");
            DropColumn("dbo.AspNetUsers", "OnboardingSkipped");
        }
    }
}
