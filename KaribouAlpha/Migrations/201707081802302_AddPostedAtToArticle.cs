namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostedAtToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "PostedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "PostedAt");
        }
    }
}
