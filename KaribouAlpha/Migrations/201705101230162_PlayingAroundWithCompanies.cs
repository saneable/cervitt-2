namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayingAroundWithCompanies : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Companies", name: "OwnerID", newName: "ID");
            RenameIndex(table: "dbo.Companies", name: "IX_OwnerID", newName: "IX_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Companies", name: "IX_ID", newName: "IX_OwnerID");
            RenameColumn(table: "dbo.Companies", name: "ID", newName: "OwnerID");
        }
    }
}
