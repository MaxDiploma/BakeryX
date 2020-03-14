namespace BakeryX.EF.Models
{
    public class Worker : BaseModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int Age { get; set; }

        public string EmailAddress { get; set; }
    }
}
