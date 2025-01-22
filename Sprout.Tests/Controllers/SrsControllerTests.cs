using Moq;
using Sprout.Web.Controllers;
using Sprout.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Sprout.Web.Contracts;
using Microsoft.AspNetCore.Http;

namespace Sprout.Tests.Controllers
{
    public class SrsControllerTests
    {

        [Fact]
        public async Task ReviewItem_NullReviewResult_ReturnsBadRequest()
        {
            // Arrange
            var mockSrsService = new Mock<ISrsService>();
            var srsController = new SrsController(mockSrsService.Object);

            // Act
            var result = await srsController.ReviewItem(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid review.", badRequestResult.Value);
        }

        [Fact]
        public async Task ReviewItem_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var mockSrsService = new Mock<ISrsService>();
            var srsController = new SrsController(mockSrsService.Object);
            var review = new ReviewDTO
            {
                SrsId = -1,
                IsCorrect = true
            };

            // Act
            var result = await srsController.ReviewItem(review);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid review.", badRequestResult.Value);
        }

        [Fact]
        public async Task ReviewItem_ValidReview_ReturnsOk()
        {
            // Arrange
            var review = new ReviewDTO
            {
                SrsId = 1,
                IsCorrect = true
            };
            var mockSrsService = new Mock<ISrsService>();
            mockSrsService
                .Setup(service => service.UpdateSrsProgressAsync(review.SrsId, review.IsCorrect))
                .Returns(Task.CompletedTask);
            var srsController = new SrsController(mockSrsService.Object);

            //Act
            var result = await srsController.ReviewItem(review);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task ReviewItem_ServiceThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var review = new ReviewDTO
            {
                SrsId = 1,
                IsCorrect = false
            };
            var mockSrsService = new Mock<ISrsService>();
            mockSrsService
                .Setup(service => service.UpdateSrsProgressAsync(review.SrsId, review.IsCorrect))
                .ThrowsAsync(new Exception("Service error"));
            var srsController = new SrsController(mockSrsService.Object);

            // Act
            var result = await srsController.ReviewItem(review);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.NotNull(errorResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);

        }
    }
}
