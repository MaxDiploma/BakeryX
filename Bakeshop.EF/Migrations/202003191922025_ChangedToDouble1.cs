namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToDouble1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredients", "Quantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingredients", "Quantity", c => c.Int(nullable: false));
        }
    }
}
