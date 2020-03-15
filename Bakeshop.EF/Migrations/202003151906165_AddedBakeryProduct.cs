namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBakeryProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BakeryProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        ReceivedDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        UomType = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BakeryProducts");
        }
    }
}
