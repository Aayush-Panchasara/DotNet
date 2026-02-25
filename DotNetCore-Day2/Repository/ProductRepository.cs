using DotNetCore_Day2.Data;
using DotNetCore_Day2.Model.Entities;
using DotNetCore_Day2.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore_Day2.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) { 
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByCategory(int category)
        {
            return _context.Products.Where(p => p.Category == (Category)category).ToList();
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) { 
                return false;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
