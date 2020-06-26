using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Helpers;
using ProductsService.Helpers.Pagination;
using ProductsService.Service.Interface;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService service, IMapper mapper)
        {
            _productsService = service;
            _mapper = mapper;
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
