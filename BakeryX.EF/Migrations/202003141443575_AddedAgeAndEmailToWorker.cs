namespace BakeryX.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAgeAndEmailToWorker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "EmailAddress", c => c.String());
            AddColumn("dbo.Suppliers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Suppliers", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "EmailAddress");
            DropColumn("dbo.Suppliers", "Age");
            DropColumn("dbo.Employees", "EmailAddress");
            DropColumn("dbo.Employees", "Age");
        }
    }
}
