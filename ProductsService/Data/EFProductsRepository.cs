using Microsoft.EntityFrameworkCore;
using ProductsService.Data.Interface;
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

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }
    }
}
