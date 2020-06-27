using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsService.Controllers.V2;
using ProductsService.Helpers.Pagination;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.UnitTests
{
    public class ProductsControllerV2Tests
    {
        [Fact]
        public async Task ProductsControllerV2_GetAll_ReturnsOk()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);
            MockResponse(controller);

            var pageSize = 10;
            var pageNumber = 2;
            var productParmeters = new ProductParmeters { PageNumber = pageNumber, PageSize = pageSize };

            // act
            var result = await controller.GetAll(productParmeters);

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV2_GetAll_ReturnsBadRequest_Pagenumber()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);
            var productParmeters = new ProductParmeters { PageNumber = -1, PageSize = 10 };

            // act
            var result = await controller.GetAll(productParmeters);

            // assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV2_GetAll_ReturnsBadRequest_PageSize()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);
            var productParmeters = new ProductParmeters { PageNumber = 1, PageSize = -1 };

            // act
            var result = await controller.GetAll(productParmeters);

            // assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        private static void MockResponse(ProductsController controller)
        {
            // https://stackoverflow.com/a/35327514
            var headerDictionary = new HeaderDictionary();
            var response = new Mock<HttpResponse>();
            response.SetupGet(r => r.Headers).Returns(headerDictionary);

            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(a => a.Response).Returns(response.Object);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext.Object
            };
        }

    }
}
