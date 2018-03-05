namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetArticleTitleToRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Title", c => c.String());
        }
    }
}
