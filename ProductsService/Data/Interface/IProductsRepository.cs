using ProductsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Data.Interface
{
    public interface IProductsRepository
    {
        public Task<Product> GetProduct(int id);
        public Task<IEnumerable<Product>> GetAllProducts();
    }
}
