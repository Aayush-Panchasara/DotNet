using DotNetCore_Day2.Model.Entities;
using DotNetCore_Day2.Repository;

namespace DotNetCore_Day2.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> getAllProduct()
        {
            return _repository.GetAll();
        }
        public Product getProductById(int id)
        {
            return _repository.GetById(id);
        }
        public List<Product> getProductBYCategory(int category)
        {
            return _repository.GetByCategory(category);
        }
        public Product addProduct(Product product)
        {
            return _repository.Add(product);
        }
        public bool deleteProduct(int id)
        {
            return _repository.Delete(id);
        }
    }
}
