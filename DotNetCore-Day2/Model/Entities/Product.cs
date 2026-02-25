using DotNetCore_Day2.Model.Enum;

namespace DotNetCore_Day2.Model.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}
