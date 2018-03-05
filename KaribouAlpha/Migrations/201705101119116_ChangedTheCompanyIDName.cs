namespace KaribouAlpha.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTheCompanyIDName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Companies", name: "ID", newName: "OwnerID");
            RenameIndex(table: "dbo.Companies", name: "IX_ID", newName: "IX_OwnerID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Companies", name: "IX_OwnerID", newName: "IX_ID");
            RenameColumn(table: "dbo.Companies", name: "OwnerID", newName: "ID");
        }
    }
}
