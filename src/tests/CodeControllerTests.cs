using fastwin.Controllers;
using fastwin.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CodeControllerTests 
{
    // Test cases created by chatgpt
    [TestClass]
    public class CodeControllerTests
    {
        [TestMethod]
        public async Task GetCodeById_ExistingCode_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetCodeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Codes { Id = 1 }); // Change the ID to a different value

            var controller = new CodeController(mediatorMock.Object);

            // Act
            var result = await controller.GetCodeById(1, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOfType(okResult.Value, typeof(Codes));
            Assert.AreEqual(1, ((Codes)okResult.Value).Id); // Change the expected ID to 1
        }

        [TestMethod]
        public async Task GetCodeById_NonExistingCode_ReturnsNotFoundResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetCodeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Codes)null);

            var controller = new CodeController(mediatorMock.Object);

            // Act
            var result = await controller.GetCodeById(1, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCodeById_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetCodeByIdQuery>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception("Something went wrong."));

            var controller = new CodeController(mediatorMock.Object);

            // Act
            var result = await controller.GetCodeById(1, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = (ObjectResult)result;
            Assert.AreEqual(500, objectResult.StatusCode);
        }
    }
}
