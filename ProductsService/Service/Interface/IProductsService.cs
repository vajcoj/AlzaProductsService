using ProductsService.DTOs;
using ProductsService.Helpers.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Service.Interface
{
    public interface IProductsService
    {
        public Task<ProductGetDTO> Get(int id);
        public Task<IEnumerable<ProductGetDTO>> Get();
        public Task<PagedList<ProductGetDTO>> Get(ProductParmeters productParmeters);
        public Task<bool> Update(int id, ProductPatchDTO patch);
    }
}
