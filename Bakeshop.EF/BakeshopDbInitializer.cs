﻿using Bakeshop.Common.Enums;
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
                        Phone = "(111)-111-111-11",
                        Password = "qwerty1"
                    },

                    new BakeshopWorker
                    {
                        Firstname = "Amazing",
                        Lastname = "Den",
                        Position = Positions.Trainee,
                        Age = 22,
                        Email = "name2@test.com",
                        Phone = "(111)-111-111-11",
                        Password = "qwerty2"
                    },

                    new BakeshopWorker
                    {
                        Firstname = "Marting",
                        Lastname = "King",
                        Position = Positions.Manager,
                        Age = 43,
                        Email = "name3@test.com",
                        Phone = "(111)-111-111-11",
                        Password = "qwerty3"
                    },

                    new BakeshopWorker
                    {
                        Firstname = "Jake",
                        Lastname = "Peralta",
                        Position = Positions.Owner,
                        Age = 11,
                        Email = "name4@test.com",
                        Phone = "(111)-111-111-11",
                        Password = "qwerty4"
                    }
            };

                context.BakeshopWorkers.AddRange(bakeshopWorkers);
            }


            var meat = new Product { Name = "Meat", Uom = UomTypes.Killograms };
            var milk = new Product { Name = "Milk", Uom = UomTypes.Litres };
            var eggs = new Product { Name = "Eggs", Uom = UomTypes.Pcs };
            var water = new Product { Name = "Water", Uom = UomTypes.Litres };
            var sausages = new Product { Name = "Sausages", Uom = UomTypes.Killograms };
            var butter = new Product { Name = "Butter", Uom = UomTypes.Killograms };
            var flour = new Product { Name = "Flour", Uom = UomTypes.Killograms };
            var sugar = new Product { Name = "Sugar", Uom = UomTypes.Killograms };
            var potato = new Product { Name = "Potato", Uom = UomTypes.Killograms };

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
                            milk,
                            sugar
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
                            water,
                            potato
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
                            butter,
                            sugar
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
                            flour,
                            potato
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
                                Name = "Meat",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Cutlet",
                    RecipeType = RecipeTypes.Traditional,
                    Price = 300
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
                                Quantity = 5,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Sausages",
                                Quantity = 0.5,
                                UomType = UomTypes.Killograms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Omelette",
                    RecipeType = RecipeTypes.Asian,
                    Price = 150
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
                                Quantity = 10,
                                UomType = UomTypes.Pcs
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Flour",
                                Quantity = 2,
                                UomType = UomTypes.Killograms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Bread",
                    RecipeType = RecipeTypes.Vegan,
                    Price = 100
                },
                new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Bread",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Butter",
                                Quantity = 100,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Sandwich",
                    RecipeType = RecipeTypes.Traditional,
                    Price = 170
                },
                new Formula
                {
                    FormulaIngredients = new List<FormulaIngredient>
                    {
                        new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Water",
                                Quantity = 1,
                                UomType = UomTypes.Litres
                            }
                        },
                          new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Meat",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        },
                            new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Eggs",
                                Quantity = 1,
                                UomType = UomTypes.Pcs
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Miso-soup",
                    RecipeType = RecipeTypes.Traditional,
                    Price = 400
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
                                Name = "Flour",
                                Quantity = 1,
                                UomType = UomTypes.Killograms
                            }
                        },
                           new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Sugar",
                                Quantity = 500,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Muffin",
                    RecipeType = RecipeTypes.Traditional,
                    Price = 350
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
                                Quantity = 5,
                                UomType = UomTypes.Pcs
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
                        },
                           new FormulaIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Sausages",
                                Quantity = 300,
                                UomType = UomTypes.Gramms
                            }
                        }
                    },
                    Description = LoremIpsum,
                    Name = "Toasts",
                    RecipeType = RecipeTypes.Traditional,
                    Price = 50
                }
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
                        Name = "Muffin",
                        Quantity = 3,
                        UomType = UomTypes.Pcs,
                        Price = 53
                    },
                    new BakeryProduct
                    {
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        Name = "Bread",
                        Quantity = 3,
                        UomType = UomTypes.Pcs,
                        Price = 12
                    },
                    new BakeryProduct
                    {
                        ExpirationDate = DateTime.UtcNow.AddDays(1),
                        Name = "Miso-soup",
                        Quantity = 3,
                        UomType = UomTypes.Litres,
                        Price = 13
                    },
                };

                context.BakeryProducts.AddRange(bakeryProducts);
            }

            if (!context.Sales.Any())
            {
                var sales = new List<Sale>
                {
                    new Sale
                    {
                        Quantity = 3,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow.AddDays(-1),
                        UomType = UomTypes.Killograms,
                        Name = "Bread"
                    },
                    new Sale
                    {
                        Quantity = 15,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow,
                        UomType = UomTypes.Killograms,
                        Name = "Bread"
                    },
                    new Sale
                    {
                        Quantity = 5,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow.AddDays(1),
                        UomType = UomTypes.Killograms,
                        Name = "Bread"
                    },
                    new Sale
                    {
                        Quantity = 5,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow.AddDays(-1),
                        UomType = UomTypes.Litres,
                        Name = "Miso-soup"
                    },
                    new Sale
                    {
                        Quantity = 10,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow,
                        UomType = UomTypes.Litres,
                        Name = "Miso-soup"
                    },
                    new Sale
                    {
                        Quantity = 20,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow.AddDays(1),
                        UomType = UomTypes.Litres,
                        Name = "Miso-soup"
                    },
                    new Sale
                    {
                        Quantity = 5,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow.AddDays(-1),
                        UomType = UomTypes.Pcs,
                        Name = "Muffin"
                    },
                        new Sale
                    {
                        Quantity = -10,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow,
                        UomType = UomTypes.Pcs,
                        Name = "Muffin"
                    },
                    new Sale
                    {
                        Quantity = -20,
                        Amount = 300,
                        TransactionDate = DateTime.UtcNow.AddDays(1),
                        UomType = UomTypes.Pcs,
                        Name = "Muffin"
                    }
                };

                context.Sales.AddRange(sales);
            }

            context.SaveChanges();
        }
    }
}
