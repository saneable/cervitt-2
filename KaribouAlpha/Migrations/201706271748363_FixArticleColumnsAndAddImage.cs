namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixArticleColumnsAndAddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Image", c => c.Binary());
            AlterColumn("dbo.Articles", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Articles", "Image");
        }
    }
}
