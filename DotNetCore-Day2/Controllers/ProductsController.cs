using AutoMapper;
using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;
using DotNetCore_Day2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.getAllProduct();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetAllProductById([FromRoute]int id)
        {
           var product = _productService.getProductById(id);

            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]
        [Route("category/{name:int}")]
        public IActionResult GetAllProductByCategory([FromRoute]int name)
        {
           var products = _productService.getProductBYCategory(name);

            if (products.Count == 0)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpPost]
        public IActionResult AddNewProduct([FromBody]ProductsDTO dto)
        {
           var newProduct = _productService.addProduct(dto);

            return Created();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult UpdateProduct([FromBody] ProductsDTO dto,[FromRoute]int id) {

            var updateProduct = _productService.getProductById(id);

            if (updateProduct == null) {
                return NotFound();
            }

            var updatedProduct = _productService.updateProduct(dto,id);

            return Ok(updatedProduct);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var deleteProduct = _productService.deleteProduct(id);

            if (!deleteProduct)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
