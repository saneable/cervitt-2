namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddURIToCompaniesAndProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "URI", c => c.String());
            AddColumn("dbo.Products", "URI", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "URI");
            DropColumn("dbo.Companies", "URI");
        }
    }
}
