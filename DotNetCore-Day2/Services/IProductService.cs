using DotNetCore_Day2.Model.Entities;

namespace DotNetCore_Day2.Services
{
    public interface IProductService
    {
        public List<Product> getAllProduct();
        public Product getProductById(int id);
        public List<Product> getProductBYCategory(int category);
        public Product addProduct(Product product);
        public bool deleteProduct(int id);
    }
}
