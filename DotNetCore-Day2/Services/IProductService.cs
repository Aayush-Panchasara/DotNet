using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;

namespace DotNetCore_Day2.Services
{
    public interface IProductService
    {
        public List<ProductsDTO> getAllProduct();
        public ProductsDTO getProductById(int id);
        public List<ProductsDTO> getProductBYCategory(int category);
        public ProductsDTO addProduct(ProductsDTO dto);
        public ProductsDTO updateProduct(ProductsDTO dto,int id);
        public bool deleteProduct(int id);
    }
}
