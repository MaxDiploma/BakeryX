namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRecipeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Formulas", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Formulas", "Name");
        }
    }
}
