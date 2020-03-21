namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedReceivedDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BakeryProducts", "ReceivedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BakeryProducts", "ReceivedDate", c => c.DateTime(nullable: false));
        }
    }
}
