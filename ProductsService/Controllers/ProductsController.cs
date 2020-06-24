using Microsoft.AspNetCore.Mvc;
using ProductsService.Models;
using System.Collections.Generic;

namespace ProductsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = new Product { Id = id, Name = "Test product", ImgUri="/img/product.png", Description="text", Price = 100 };
            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = new List<Product>(){
                new Product { Id = 1, Name = "Notebook", ImgUri="/img/product.png", Description="text", Price = 44_999 },
                new Product { Id = 2, Name = "Keyboard", ImgUri="/img/product.png", Description="text", Price = 2_399 },
                new Product { Id = 3, Name = "Mouse", ImgUri="/img/product.png", Description="text", Price = 499 }
            };

            return Ok(products);
        }


    }
}
