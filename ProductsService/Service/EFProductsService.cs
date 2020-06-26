using Microsoft.EntityFrameworkCore;
using ProductsService.Service.Interface;
using ProductsService.Helpers.Pagination;
using ProductsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsService.Data;
using AutoMapper;
using ProductsService.DTOs;

namespace ProductsService.Service
{
    public class EFProductsService : IProductsService
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public EFProductsService(ProductsContext context, IMapper mapper)
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

        public async Task<PagedList<ProductGetDTO>> Get(ProductParmeters productParmeters)
        {
            var products = await PagedList<Product>.ToPagedList(_context.Products,
                    productParmeters.PageNumber,
                    productParmeters.PageSize);

            var productsToReturn = _mapper.Map<PagedList<ProductGetDTO>>(products);

            return productsToReturn;
        }

        public async Task<ProductGetDTO> Get(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var productToReturn = _mapper.Map<ProductGetDTO>(product);
            return productToReturn;
        }


        public async Task<bool> Update(int id, ProductPatchDTO patch)
        {
            var product = new Product() { Id = id};

            _mapper.Map(patch, product);
            _context.Products.Attach(product);
            _context.Entry(product).Property(x => x.Description).IsModified = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}


