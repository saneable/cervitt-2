namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToCompanyMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyMembers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyMembers", "Email");
        }
    }
}
