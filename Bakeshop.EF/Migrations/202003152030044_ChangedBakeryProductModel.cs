namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBakeryProductModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "TransactionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "TransactionDate", c => c.String());
        }
    }
}
