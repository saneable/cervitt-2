namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrivacyToProductFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "Privacy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductFiles", "Privacy");
        }
    }
}
