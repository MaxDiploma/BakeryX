namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPriceToFormula : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formulas", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Formulas", "Price");
        }
    }
}
