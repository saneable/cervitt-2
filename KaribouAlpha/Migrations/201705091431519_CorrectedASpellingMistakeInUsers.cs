namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedASpellingMistakeInUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FacebookUrl", c => c.String());
            DropColumn("dbo.AspNetUsers", "FacebooknUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "FacebooknUrl", c => c.String());
            DropColumn("dbo.AspNetUsers", "FacebookUrl");
        }
    }
}
