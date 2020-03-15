using Bakeshop.Common.Enums;

namespace Bakeshop.DomainModels
{
    public class SupplierDomain
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Positions Position { get; set; }

        public string Phone { get; set; }

        public string Products { get; set; }
    }
}
