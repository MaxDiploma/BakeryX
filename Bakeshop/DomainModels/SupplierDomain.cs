using Bakeshop.Common.Enums;
using System;

namespace Bakeshop.DomainModels
{
    public class SupplierDomain
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Positions Position { get; set; }

        public string Phone { get; set; }

        public string Products { get; set; }
    }
}
