using Microsoft.AspNetCore.Mvc;
using ProductsService.Helpers;
using ProductsService.Helpers.Pagination;
using ProductsService.Services.Interface;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService service)
        {
            _productsService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductParmeters productParmeters)
        {
            var products = await _productsService.Get(productParmeters);          

            Response.AddPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages, products.HasNext, products.HasPrevious);
           
            return Ok(products);
        }

    }
}
