namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToProductFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductFiles", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductFiles", "Image");
        }
    }
}
