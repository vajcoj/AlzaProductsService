using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductsService.DTOs;
using ProductsService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService service)
        {
            _productsService = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            var product = await _productsService.Get(id);

            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductGetDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsService.Get(); 

            return Ok(products);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PartialUpdate(int id, JsonPatchDocument<ProductPatchDTO> patchDoc)
        {
            if (id < 1) return BadRequest();

            if (patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var patch = new ProductPatchDTO();

            patchDoc.ApplyTo(patch, ModelState);

            if (!TryValidateModel(patch))
            {
                return ValidationProblem(ModelState);
            }

            if (! await _productsService.Update(id, patch))
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
