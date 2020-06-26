using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.DTOs;
using ProductsService.Helpers.Pagination;
using ProductsService.Models;
using ProductsService.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public ProductService(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductGetDTO>> Get()
        {
            var products = await _context.Products.ToListAsync();
            var productsToReturn = _mapper.Map<IEnumerable<ProductGetDTO>>(products);
            return productsToReturn;
        }

        public async Task<PagedList<ProductGetDTO>> GetPaged(ProductParmeters productParmeters)
        {
            return await _context.Products
                .Select(p => _mapper.Map<ProductGetDTO>(p))
                .ToPagedList(productParmeters);
        }

        public async Task<ProductGetDTO> Get(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var productToReturn = _mapper.Map<ProductGetDTO>(product);
            return productToReturn;
        }

        public async Task<bool> Update(int id, ProductPatchDTO patch)
        {
            var product = new Product() { Id = id };

            _mapper.Map(patch, product);
            _context.Products.Attach(product);
            _context.Entry(product).Property(x => x.Description).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}


