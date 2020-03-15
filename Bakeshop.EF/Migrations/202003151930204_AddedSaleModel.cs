namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSaleModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Int(nullable: false),
                        TransactionDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BakeryProducts", "IsSold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BakeryProducts", "IsSold");
            DropTable("dbo.Sales");
        }
    }
}
