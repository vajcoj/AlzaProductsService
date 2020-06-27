using Microsoft.EntityFrameworkCore;
using ProductsService.DTOs;
using ProductsService.Helpers.Pagination;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.UnitTests
{
    public class ProductsServiceTests
    {
        [Fact]
        public async Task ProductService_ExistingId()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var id = 1;

            // act
            var product = await service.Get(id);

            // assert
            Assert.NotNull(product);
            Assert.Equal(id, product.Id);
        }

        [Fact]
        public async Task ProductService_NonExistingId()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var id = 999;

            // act
            var product = await service.Get(id);

            // assert
            Assert.Null(product);
        }

        [Fact]
        public async Task ProductService_PartialUpdate_ChangesValue()
        {
            // arrange
            var context = InfrastructureBuilder.GetContext();
            var service = InfrastructureBuilder.GetProductService(context);
            int id = 1;
            string value = "changed value";
            var patch = new ProductPatchDTO { Description = value };

            // act
            var result = await service.Update(id, patch);

            // assert
            Assert.True(result);
            var dbValue = await context.Products.AsNoTracking().Where(w => w.Id == id).Select(p => p.Description).FirstOrDefaultAsync();
            Assert.Equal(value, dbValue);
        }

        [Fact]
        public async Task ProductService_GetAllPaged_PageSizeOk()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();

            var pageSize = 10;
            var pageNumber = 2;
            var productParameters = new ProductParmeters { PageNumber = pageNumber, PageSize = pageSize };

            // act
            var result = await service.GetPaged(productParameters);

            // assert
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.Count());

        }

    }



}
