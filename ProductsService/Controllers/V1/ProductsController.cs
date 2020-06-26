﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Service.Interface;
using ProductsService.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService service, IMapper mapper)
        {
            _productsService = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productsService.Get(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsService.Get(); 
            return Ok(products);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdate(int id, JsonPatchDocument<ProductPatchDTO> patchDoc)
        {
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
                throw new Exception($"Updating product {id} failed.");
            }

            return NoContent();
        }

    }
}
