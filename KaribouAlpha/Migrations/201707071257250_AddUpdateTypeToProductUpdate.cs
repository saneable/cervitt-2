namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdateTypeToProductUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UpdateType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "UpdateType");
        }
    }
}
