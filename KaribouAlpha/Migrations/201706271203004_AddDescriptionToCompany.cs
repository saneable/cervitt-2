namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Description");
        }
    }
}
