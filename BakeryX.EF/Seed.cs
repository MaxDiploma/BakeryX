using BakeryX.Common.Enums;
using BakeryX.EF.Models;
using System.Collections.Generic;
using System.Linq;

namespace BakeryX.EF
{
    public static class Seed
    {
        private static BakeryXContext _bakeryXContext;

        static Seed()
        {
            _bakeryXContext = new BakeryXContext();
        }

        public static void SeedData(BakeryXContext context)
        {
            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Firstname = "John",
                        Lastname = "Berg",
                        Position = EmployeePositions.Cook,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Amazing",
                        Lastname = "Den",
                        Position = EmployeePositions.CookTrainee,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Marting",
                        Lastname = "King",
                        Position = EmployeePositions.Manager,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Super",
                        Lastname = "Admin",
                        Position = EmployeePositions.Admin,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },
                      new Employee
                    {
                        Firstname = "John",
                        Lastname = "Berg",
                        Position = EmployeePositions.Cook,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Amazing",
                        Lastname = "Den",
                        Position = EmployeePositions.CookTrainee,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Marting",
                        Lastname = "King",
                        Position = EmployeePositions.Manager,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },
                      new Employee
                    {
                        Firstname = "John",
                        Lastname = "Berg",
                        Position = EmployeePositions.Cook,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Amazing",
                        Lastname = "Den",
                        Position = EmployeePositions.CookTrainee,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Marting",
                        Lastname = "King",
                        Position = EmployeePositions.Manager,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "John",
                        Lastname = "Berg",
                        Position = EmployeePositions.Cook,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Amazing",
                        Lastname = "Den",
                        Position = EmployeePositions.CookTrainee,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    },

                    new Employee
                    {
                        Firstname = "Marting",
                        Lastname = "King",
                        Position = EmployeePositions.Manager,
                        Age = 23,
                        EmailAddress = "name@test.com"
                    }
                };

                _bakeryXContext.Employees.AddRange(employees);
            }

            _bakeryXContext.SaveChanges();
        }
    }
}
