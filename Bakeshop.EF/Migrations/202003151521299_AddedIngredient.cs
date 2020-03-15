namespace Bakeshop.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIngredient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormulaIngredients",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FormulaId = c.Guid(nullable: false),
                        IngredientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Formulas", t => t.FormulaId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.FormulaId)
                .Index(t => t.IngredientId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        UomType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Formulas", "RecipeType", c => c.Int(nullable: false));
            AddColumn("dbo.Formulas", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormulaIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.FormulaIngredients", "FormulaId", "dbo.Formulas");
            DropIndex("dbo.FormulaIngredients", new[] { "IngredientId" });
            DropIndex("dbo.FormulaIngredients", new[] { "FormulaId" });
            DropColumn("dbo.Formulas", "Description");
            DropColumn("dbo.Formulas", "RecipeType");
            DropTable("dbo.Ingredients");
            DropTable("dbo.FormulaIngredients");
        }
    }
}
