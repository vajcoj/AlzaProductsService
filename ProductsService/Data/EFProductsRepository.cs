using Microsoft.EntityFrameworkCore;
using ProductsService.Data.Interface;
using ProductsService.Helpers.Pagination;
using ProductsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Data
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly ProductsContext _context;

        public EFProductsRepository(ProductsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<PagedList<Product>> GetAllProductsPaged(ProductParmeters productParmeters)
        {
            var products = await PagedList<Product>.ToPagedList(_context.Products,
                    productParmeters.PageNumber,
                    productParmeters.PageSize);

            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
