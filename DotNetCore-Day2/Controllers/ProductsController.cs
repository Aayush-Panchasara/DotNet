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
        public IActionResult AddNewProduct([FromBody]Product product)
        {
           var newProduct = _productService.addProduct(product);

            return CreatedAtAction(
                nameof(GetAllProductById),
                new { id = newProduct.Id },
                newProduct
                );
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
