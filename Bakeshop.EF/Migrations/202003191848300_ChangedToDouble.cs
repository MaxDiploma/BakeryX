namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BakeshopProducts", "Quantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BakeshopProducts", "Quantity", c => c.Int(nullable: false));
        }
    }
}
