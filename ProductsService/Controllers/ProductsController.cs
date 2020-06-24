using Microsoft.AspNetCore.Mvc;
using ProductsService.Data.Interface;
using System.Threading.Tasks;

namespace ProductsService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepo;

        public ProductsController(IProductsRepository repo)
        {
            _productsRepo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productsRepo.GetProduct(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsRepo.GetAllProducts();
            return Ok(products);
        }


    }
}
