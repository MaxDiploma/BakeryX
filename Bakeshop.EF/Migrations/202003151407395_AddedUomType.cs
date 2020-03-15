namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUomType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BakeshopProducts",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        SupplierId = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ReceivedDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        UomType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BakeshopProducts");
        }
    }
}
