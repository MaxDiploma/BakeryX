namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRelationFromFormulas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductsFormulas", "FormulaId", "dbo.Formulas");
            DropForeignKey("dbo.ProductsFormulas", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductsFormulas", new[] { "FormulaId" });
            DropIndex("dbo.ProductsFormulas", new[] { "ProductId" });
            DropTable("dbo.ProductsFormulas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductsFormulas",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FormulaId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ProductsFormulas", "ProductId");
            CreateIndex("dbo.ProductsFormulas", "FormulaId");
            AddForeignKey("dbo.ProductsFormulas", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductsFormulas", "FormulaId", "dbo.Formulas", "Id", cascadeDelete: true);
        }
    }
}
