namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameOrganizationsToCompanies : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Organizations", newName: "Companies");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Companies", newName: "Organizations");
        }
    }
}
