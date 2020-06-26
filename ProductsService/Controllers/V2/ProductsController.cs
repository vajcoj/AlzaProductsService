using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsService.DTOs;
using ProductsService.Helpers;
using ProductsService.Helpers.Pagination;
using ProductsService.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService service)
        {
            _productsService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductGetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] ProductParmeters productParmeters)
        {
            var products = await _productsService.GetPaged(productParmeters);

            Response.AddPagination(products);

            return Ok(products);
        }

    }
}
