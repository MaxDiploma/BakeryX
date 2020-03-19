namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedSaleModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "UomType", c => c.Int(nullable: false));
            AddColumn("dbo.Sales", "Quantity", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "Quantity");
            DropColumn("dbo.Sales", "UomType");
        }
    }
}
