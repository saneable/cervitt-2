namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdatedAtToNewsItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "UpdatedAt");
        }
    }
}
