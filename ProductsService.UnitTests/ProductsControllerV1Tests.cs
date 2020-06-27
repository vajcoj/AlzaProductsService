using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using ProductsService.Controllers.V1;
using ProductsService.DTOs;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.UnitTests
{
    public class ProductsControllerV1Tests
    {
        [Fact]
        public async Task ProductsControllerV1_Get_ReturnsOk()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);
            var id = 1;

            // act
            var result = await controller.Get(id);

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_Get_ReturnsBadRequest()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);
            var id = -1;

            // act
            var result = await controller.Get(id);

            // assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_Get_ReturnsNotFound()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);
            var id = 999;

            // act
            var result = await controller.Get(id);

            // assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_GetAll_ReturnsOk()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);

            // act
            var result = await controller.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_PartialUpdate_ReturnsNocontent()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);

            var patchDoc = new JsonPatchDocument<ProductPatchDTO>();
            patchDoc.Replace<string>(s => s.Description, "new value");

            MockProductPatchDTOValidator(controller);

            // act
            var result = await controller.PartialUpdate(1, patchDoc);

            // assert
            Assert.NotNull(result);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_PartialUpdate_ReturnsBadRequest_NegativeId()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);

            var patchDoc = new JsonPatchDocument<ProductPatchDTO>();
            patchDoc.Replace<string>(s => s.Description, "new value");
            MockProductPatchDTOValidator(controller);

            // act
            var result = await controller.PartialUpdate(-1, patchDoc);

            // assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_PartialUpdate_ReturnsBadRequest_NullPatch()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);

            MockProductPatchDTOValidator(controller);

            // act
            var result = await controller.PartialUpdate(-1, null);

            // assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ProductsControllerV1_PartialUpdate_ReturnsNotFound_NonExistingId()
        {
            // arrange
            var service = InfrastructureBuilder.GetProductService();
            var controller = new ProductsController(service);

            MockProductPatchDTOValidator(controller);

            // act
            var result = await controller.PartialUpdate(999, null);

            // assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        private static void MockProductPatchDTOValidator(ProductsController controller)
        {
            // https://github.com/aspnet/Mvc/issues/3586
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                                              It.IsAny<ValidationStateDictionary>(),
                                              It.IsAny<string>(),
                                              It.IsAny<ProductPatchDTO>()));
            controller.ObjectValidator = objectValidator.Object;
        }

    }
}
