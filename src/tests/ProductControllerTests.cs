using fastwin.Controllers;
using fastwin.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentValidation;

namespace ProductControllerTest
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public async Task AddProduct_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mediatorMock.Object);
            var productRequest = new ProductRequest
            {
                Name = "Sample Product",
                Description = "This is a sample product description.",
                Category = "Electronics", // Assuming you have "Electronics" in your Category enum
                LastUsageDate = DateTime.UtcNow,
            };

            // Act
            var result = await controller.AddProduct(productRequest, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(ProductRequest));
            Assert.AreEqual(productRequest, okResult.Value);
        }

        [TestMethod]
        public async Task AddProduct_InvalidProduct_ReturnsBadRequestResultWithValidationErrors()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mediatorMock.Object);
            var productRequest = new ProductRequest
            {
                Name = "Sample Product",
                Description = "This is a sample product description.",
                Category = "Electronics", // Assuming you have "Electronics" in your Category enum
                LastUsageDate = DateTime.UtcNow,
            };
            controller.ModelState.AddModelError("PropertyName", "Validation error message");

            // Act
            var result = await controller.AddProduct(productRequest, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(List<string>));
            var validationErrors = (List<string>)badRequestResult.Value;
            Assert.IsTrue(validationErrors.Any());
        }

        [TestMethod]
        public async Task AddProduct_ValidationException_ReturnsBadRequestResultWithExceptionMessage()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mediatorMock.Object);
            var productRequest = new ProductRequest
            {
                Name = "Sample Product",
                Description = "This is a sample product description.",
                Category = "Electronics", // Assuming you have "Electronics" in your Category enum
                LastUsageDate = DateTime.UtcNow,
            };
            mediatorMock.Setup(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ValidationException("Validation exception message"));

            // Act
            var result = await controller.AddProduct(productRequest, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.IsInstanceOfType(badRequestResult.Value, typeof(string));
            var errorMessage = (string)badRequestResult.Value;
            Assert.AreEqual("Validation exception message", errorMessage);
        }

        [TestMethod]
        public async Task AddProduct_Exception_ReturnsInternalServerErrorResultWithExceptionMessage()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ProductController(mediatorMock.Object);
            var productRequest = new ProductRequest
            {
                Name = "Sample Product",
                Description = "This is a sample product description.",
                Category = "Electronics", // Assuming you have "Electronics" in your Category enum
                LastUsageDate = DateTime.UtcNow,
            };
            mediatorMock.Setup(x => x.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Internal server error message"));

            // Act
            var result = await controller.AddProduct(productRequest, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var internalServerErrorResult = (ObjectResult)result;
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.IsInstanceOfType(internalServerErrorResult.Value, typeof(string));
            var errorMessage = (string)internalServerErrorResult.Value;
            Assert.AreEqual("Internal server error: Internal server error message", errorMessage);
        }
    }
}