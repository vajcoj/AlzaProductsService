using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Data.Interface;
using ProductsService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository repo, IMapper mapper)
        {
            _productsRepo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsPaged()
        {
            var products = await _productsRepo.GetAllProducts();
            var productsToReturn = _mapper.Map<IEnumerable<ProductGetDTO>>(products);
            return Ok(productsToReturn);
        }

    }
}
