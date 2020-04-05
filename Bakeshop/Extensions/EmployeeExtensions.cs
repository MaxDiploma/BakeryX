using Bakeshop.DomainModels;
using Bakeshop.EF.Models;

namespace Bakeshop.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeDomain ToDomain(this BakeshopWorker worker)
        {
            return new EmployeeDomain
            {
                Age = worker.Age,
                Email = worker.Email,
                Firstname = worker.Firstname,
                Id = worker.Id,
                Lastname = worker.Lastname,
                Password = worker.Password,
                Phone = worker.Phone,
                Position = worker.Position
            };
        }
    }
}
