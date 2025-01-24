using Moq;
using Sprout.Web.Services;
using Sprout.Web.Data.Entities.Srs;

namespace Sprout.Tests.UnitTests.Services
{
    public class SrsServiceTests
    {
        [Fact]
        public async Task UpdateSrsProgressAsync_WhenCorrectAnswer_IncreasesProgressLevel()
        {
            // Arrange
            var mockRepository = new Mock<ISrsDataRepository>();
            var srsService = new SrsService(mockRepository.Object);

            var srsData = new SrsData { Id = 1, ProgressLevel = 1 };
            mockRepository.Setup(r => r.GetSrsDatabyIdAsync(1))
                .ReturnsAsync(srsData);

            // Act
            await srsService.UpdateSrsProgressAsync(1, true);

            // Assert
            Assert.Equal(2, srsData.ProgressLevel);
            mockRepository.Verify(r => r.UpdateSrsDataAsync(srsData), Times.Once);
        }

        [Fact]
        public async Task UpdateSrsProgressAsync_WhenIncorrectAnswer_ResetsProgressLevelToOne()
        {
            // Arrange
            var mockRepository = new Mock<ISrsDataRepository>();
            var srsService = new SrsService(mockRepository.Object);

            var srsData = new SrsData { Id = 1, ProgressLevel = 3 };
            mockRepository.Setup(r => r.GetSrsDatabyIdAsync(1))
                .ReturnsAsync(srsData);

            // Act
            await srsService.UpdateSrsProgressAsync(1, false);

            // Assert
            Assert.Equal(1, srsData.ProgressLevel);
            mockRepository.Verify(r => r.UpdateSrsDataAsync(srsData), Times.Once);
        }

        [Fact]
        public async Task UpdateSrsProgressAsync_WhenFirstReview_SetsFirstReviewDate()
        {
            // Arrange
            var mockRepository = new Mock<ISrsDataRepository>();
            var srsService = new SrsService(mockRepository.Object);

            var srsData = new SrsData { Id = 1, FirstReview = null };
            mockRepository.Setup(r => r.GetSrsDatabyIdAsync(1))
                .ReturnsAsync(srsData);

            // Act
            await srsService.UpdateSrsProgressAsync(1, true);

            // Assert
            Assert.NotNull(srsData.FirstReview);
            Assert.True(DateTime.Now.Subtract(srsData.FirstReview.Value).TotalSeconds < 1);
        }

        [Fact]
        public async Task UpdateSrsProgressAsync_Always_UpdatesLastReviewedDate()
        {
            // Arrange
            var mockRepository = new Mock<ISrsDataRepository>();
            var srsService = new SrsService(mockRepository.Object);

            var srsData = new SrsData { Id = 1 };
            mockRepository.Setup(r => r.GetSrsDatabyIdAsync(1))
                .ReturnsAsync(srsData);

            // Act
            await srsService.UpdateSrsProgressAsync(1, true);

            // Assert
            Assert.NotNull(srsData.LastReviewed);
            Assert.True(DateTime.Now.Subtract((DateTime)srsData.LastReviewed).TotalSeconds < 1);
        }

        [Fact]
        public async Task UpdateSrsProgressAsync_WhenSrsDataNotFound_ThrowsException()
        {
            // Arrange
            var mockRepository = new Mock<ISrsDataRepository>();
            var srsService = new SrsService(mockRepository.Object);

            mockRepository.Setup(r => r.GetSrsDatabyIdAsync(1))
                .ReturnsAsync((SrsData)null);

            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() =>
                srsService.UpdateSrsProgressAsync(1, true));
        }
    }
}
