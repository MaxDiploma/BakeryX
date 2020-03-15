namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BakeshopWorkers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formulas",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductsFormulas",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FormulaId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Formulas", t => t.FormulaId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FormulaId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Supplier_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Supplier_Id", "dbo.Suppliers");
            DropForeignKey("dbo.ProductsFormulas", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductsFormulas", "FormulaId", "dbo.Formulas");
            DropIndex("dbo.Products", new[] { "Supplier_Id" });
            DropIndex("dbo.ProductsFormulas", new[] { "ProductId" });
            DropIndex("dbo.ProductsFormulas", new[] { "FormulaId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Products");
            DropTable("dbo.ProductsFormulas");
            DropTable("dbo.Formulas");
            DropTable("dbo.BakeshopWorkers");
        }
    }
}
