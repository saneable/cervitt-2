namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFieldsFromCompany : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "DisplayName", c => c.String());
            AlterColumn("dbo.Companies", "TradingName", c => c.String());
            AlterColumn("dbo.Companies", "Email", c => c.String());
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Companies", "Sector", c => c.String());
            AlterColumn("dbo.Companies", "Country", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Companies", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Sector", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "TradingName", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "DisplayName", c => c.String(nullable: false));
        }
    }
}
