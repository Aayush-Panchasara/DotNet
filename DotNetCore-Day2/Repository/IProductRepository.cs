using DotNetCore_Day2.Model.Entities;

namespace DotNetCore_Day2.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product GetById(int id);
        public List<Product> GetByCategory(int category);

        public Product Add(Product product);

        public bool Delete(int id);
    }
}
