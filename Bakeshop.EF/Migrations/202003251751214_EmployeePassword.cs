namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeePassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BakeshopWorkers", "Password", c => c.String());
            AddColumn("dbo.Suppliers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "Password");
            DropColumn("dbo.BakeshopWorkers", "Password");
        }
    }
}
