using AutoMapper;
using DotNetCore_Day2.DTOs;
using DotNetCore_Day2.Model.Entities;
using DotNetCore_Day2.Services;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "admin,customer,vender")]
        [Authorize(Policy = "AllowAll")]

        public IActionResult GetAllProducts()
        {
            var products = _productService.getAllProduct();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin,customer,vender")]
        [Authorize(Policy = "AllowAll")]

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
        //[Authorize(Roles = "admin,customer,vender")]
        [Authorize(Policy = "AllowAll")]

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
        //[Authorize(Roles = "admin,vender")]
        [Authorize(Policy = "Admin&Vender")]

        public IActionResult AddNewProduct([FromBody]ProductsDTO dto)
        {
           var newProduct = _productService.addProduct(dto);

            return Created();
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin,vender")]
        [Authorize(Policy = "Admin&Vender")]


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
        //[Authorize(Roles = "admin")]
        [Authorize(Policy = "AdminOnly")]


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
