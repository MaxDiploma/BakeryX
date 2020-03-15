using Bakeshop.Common.Enums;
using Bakeshop.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bakeshop.EF
{
    public static class BakeshopDbInitializer
    {
        private static BakeshopContext context;
        private const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        static BakeshopDbInitializer()
        {
            context = new BakeshopContext();
        }

        public static void Seed()
        {
            if (!context.BakeshopWorkers.Any())
            {
                var bakeshopWorkers = new List<BakeshopWorker>
            {
                  new BakeshopWorker
                    {
                        Firstname = "John",
                        Lastname = "Berg",
                        Position = Positions.Cook,
                        Age = 23,
                        Email = "name1@test.com",
                        Phone = "(111)-111-111-11"
                    },

                    new BakeshopWorker
                    {
                        Firstname = "Amazing",
                        Lastname = "Den",
                        Position = Positions.Trainee,
                        Age = 22,
                        Email = "name2@test.com",
                        Phone = "(111)-111-111-11"
                    },

                    new BakeshopWorker
                    {
                        Firstname = "Marting",
                        Lastname = "King",
                        Position = Positions.Manager,
                        Age = 43,
                        Email = "name3@test.com",
                        Phone = "(111)-111-111-11"
                    },

                    new BakeshopWorker
                    {
                        Firstname = "Jake",
                        Lastname = "Peralta",
                        Position = Positions.Owner,
                        Age = 11,
                        Email = "name4@test.com",
                        Phone = "(111)-111-111-11"
                    }
            };

                context.BakeshopWorkers.AddRange(bakeshopWorkers);
            }


            var meat = new Product { Name = "Meat" };
            var milk = new Product { Name = "Milk" };
            var eggs = new Product { Name = "Eggs" };
            var water = new Product { Name = "Water" };
            var sausages = new Product { Name = "Sausages" };
            var butter = new Product { Name = "Butter" };
            var flour = new Product { Name = "flour" };

            var supplier1 = new Supplier
            {
                Firstname = "John",
                Lastname = "Berg",
                Position = Positions.Supplier,
                Age = 23,
                Email = "name1@test.com",
                Products = new List<Product>
                        {
                            meat,
                            milk
                        },
                Phone = "(111)-111-111-11"
            };

            var supplier2 = new Supplier
            {
                Firstname = "Amazing",
                Lastname = "Den",
                Position = Positions.Supplier,
                Age = 22,
                Email = "name2@test.com",
                Products = new List<Product>
                        {
                            eggs,
                            water
                        },
                Phone = "(111)-111-111-11"
            };

            var supplier3 = new Supplier
            {
                Firstname = "Marting",
                Lastname = "King",
                Position = Positions.Supplier,
                Age = 43,
                Email = "name3@test.com",
                Products = new List<Product>
                        {
                            sausages,
                            butter
                        },
                Phone = "(111)-111-111-11"
            };

            var supplier4 = new Supplier
            {
                Firstname = "Jake",
                Lastname = "Peralta",
                Position = Positions.Supplier,
                Age = 11,
                Email = "name4@test.com",
                Products = new List<Product>
                        {
                            flour
                        },
                Phone = "(111)-111-111-11"
            };

            if (!context.Suppliers.Any())
            {

                var suppliers = new List<Supplier>
                {
                    supplier1,
                    supplier2,
                    supplier3,
                    supplier4
                };

                context.Suppliers.AddRange(suppliers);
            }


            if (!context.Formulas.Any())
            {
                var formulas = new List<Formula>
            {
                new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "KOTLETA",
                    RecipeType = RecipeTypes.Traditional
                },
                new Formula
                {

                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Meat",
                                Quantity = 1,
                                UomType = UomTypes.Killograms
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Flour",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "BORSH",
                    RecipeType = RecipeTypes.Asian
                },
                new Formula
                {

                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Sausages",
                                Quantity = 4,
                                UomType = UomTypes.Gramms
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Butter",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "PAMPUSHKA",
                    RecipeType = RecipeTypes.Vegan
                },
                new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SUSHI",
                    RecipeType = RecipeTypes.Traditional
                },
                new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "MISOSUP",
                    RecipeType = RecipeTypes.Traditional
                },
                new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                 new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                  new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                   new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                    new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                     new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                      new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                       new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
                        new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 4,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Milk",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "SHLAPA",
                    RecipeType = RecipeTypes.Traditional
                },
            };

                context.Formulas.AddRange(formulas);
            }

            context.SaveChanges();

            if (!context.BakeshopProducts.Any())
            {
                var products = new List<BakeshopProduct>
                {
                    new BakeshopProduct
                    {
                        Name = "Flour",
                        Quantity = 33,
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        ReceivedDate = DateTime.UtcNow.AddDays(-1),
                        SupplierId = supplier1.Id,
                        UomType = UomTypes.Killograms
                    },
                    new BakeshopProduct
                    {
                        Name = "Meat",
                        Quantity = 21,
                        ExpirationDate = DateTime.UtcNow,
                        ReceivedDate = DateTime.UtcNow.AddDays(-1),
                        SupplierId = supplier2.Id,
                        UomType = UomTypes.Killograms
                    },
                    new BakeshopProduct
                    {
                        Name = "Milk",
                        Quantity = 10,
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        ReceivedDate = DateTime.UtcNow.AddDays(-1),
                        SupplierId = supplier3.Id,
                        UomType = UomTypes.Litres
                    },
                    new BakeshopProduct
                    {
                        Name = "Eggs",
                        Quantity = 1000,
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        ReceivedDate = DateTime.UtcNow.AddDays(-1),
                        SupplierId = supplier4.Id,
                        UomType = UomTypes.Pcs
                    }
                };

                context.BakeshopProducts.AddRange(products);
            }

            if (!context.BakeryProducts.Any())
            {
                var bakeryProducts = new List<BakeryProduct>
                {
                    new BakeryProduct
                    {
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        Name = "KOTLETA",
                        Quantity = 3,
                        UomType = UomTypes.Pcs,
                        ReceivedDate = DateTime.UtcNow,
                        Price = 333
                    },
                    new BakeryProduct
                    {
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        Name = "SUSHI",
                        Quantity = 3,
                        UomType = UomTypes.Pcs,
                        ReceivedDate = DateTime.UtcNow,
                        Price = 12
                    },
                    new BakeryProduct
                    {
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        Name = "BORSH",
                        Quantity = 3,
                        UomType = UomTypes.Litres,
                        ReceivedDate = DateTime.UtcNow,
                        Price = 444
                    },
                };

                context.BakeryProducts.AddRange(bakeryProducts);
            }

            context.SaveChanges();
        }
    }
}
