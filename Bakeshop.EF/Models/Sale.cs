using System;

namespace Bakeshop.EF.Models
{
    public class Sale : Base
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
