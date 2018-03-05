namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogoToProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Logo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Logo");
        }
    }
}
