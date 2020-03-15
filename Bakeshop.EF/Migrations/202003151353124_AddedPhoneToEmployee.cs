namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhoneToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BakeshopWorkers", "Phone", c => c.String());
            AddColumn("dbo.Suppliers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "Phone");
            DropColumn("dbo.BakeshopWorkers", "Phone");
        }
    }
}
