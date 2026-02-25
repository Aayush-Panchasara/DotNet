using DotNetCore_Day2.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore_Day2.DTOs
{
    public class ProductsDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(100,100000)]
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}
