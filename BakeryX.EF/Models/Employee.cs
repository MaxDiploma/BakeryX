using BakeryX.Common.Enums;

namespace BakeryX.EF.Models
{
    public class Employee : Worker
    {
        public EmployeePositions Position { get; set; }
    }
}
