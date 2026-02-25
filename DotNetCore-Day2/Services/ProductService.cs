using AutoMapper;
using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;
using DotNetCore_Day2.Repository;

namespace DotNetCore_Day2.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ProductsDTO> getAllProduct()
        {
            var products = _repository.GetAll(); 
            return _mapper.Map<List<ProductsDTO>>(products);
        }
        public ProductsDTO getProductById(int id)
        {
            var product = _repository.GetById(id);
            return _mapper.Map<ProductsDTO>(product);
        }
        public List<ProductsDTO> getProductBYCategory(int category)
        {
            var products = _repository.GetByCategory(category);
            return _mapper.Map<List<ProductsDTO>>(products); 
        }
        public ProductsDTO addProduct(ProductsDTO dto)
        {
            var product = _mapper.Map<Product>(dto);

            var addProduct = _repository.Add(product);
            return _mapper.Map<ProductsDTO>(addProduct);
        }

        public ProductsDTO updateProduct(ProductsDTO dto,int id)
        {
            var product = _mapper.Map<Product>(dto);

            var updatedProduct = _repository.Update(product,id);
            return _mapper.Map<ProductsDTO>(updatedProduct);
        }

        public bool deleteProduct(int id)
        {
            return _repository.Delete(id);
        }
    }
}
