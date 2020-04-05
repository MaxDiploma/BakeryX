namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUomsToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Uom", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Uom");
        }
    }
}
