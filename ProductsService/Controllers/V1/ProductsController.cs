using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Data.Interface;
using ProductsService.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository repo, IMapper mapper)
        {
            _productsRepo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productsRepo.GetProduct(id);
            var productToReturn = _mapper.Map<ProductGetDTO>(product);
            return Ok(productToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsRepo.GetAllProducts();
            var productsToReturn = _mapper.Map<IEnumerable<ProductGetDTO>>(products);
            return Ok(productsToReturn);
        }

    }
}
