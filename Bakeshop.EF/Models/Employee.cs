using Bakeshop.Common.Enums;

namespace Bakeshop.EF.Models
{
    public class Employee : Base
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public Positions Position { get; set; }

        public string Phone { get; set; }
    }
}
