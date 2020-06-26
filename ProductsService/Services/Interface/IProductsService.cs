using ProductsService.DTOs;
using ProductsService.Helpers.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsService.Services.Interface
{
    public interface IProductsService
    {
        public Task<ProductGetDTO> Get(int id);
        public Task<IEnumerable<ProductGetDTO>> Get();
        public Task<PagedList<ProductGetDTO>> Get(ProductParmeters productParmeters);

        /// <summary>
        /// Updates product with given id by patch data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns>Returns true if product was updated, false otherwise.</returns>
        public Task<bool> Update(int id, ProductPatchDTO patch);
    }
}
