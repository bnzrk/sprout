using Xunit;
using Moq;
using Sprout.Web.Controllers;
using Sprout.Web.Services;
using Sprout.Web.Data.Entities.Kanji;
using Sprout.Web.Models;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace Sprout.Tests.Controllers
{
    public class KanjiControllerTests
    {
        [Fact]
        public async void GetKanjiByLiteral_ReturnsOk_WhenKanjiExists()
        {
            var literal = "芽";
            var meanings = new List<string> { "bud", "sprout", "spear", "germ" };
            var kunReadings = new List<string> { "め" };
            var onReadings = new List<string> { "ガ" };
            var nanoriReadings = new List<string> { "じ", "めぐ" };
            var grade = 4;
            var jlptLevel = 1;
            var strokeCount = 8;
            var frequency = 1691;

            var mockService = new Mock<IKanjiService>();
            var kanji = new Kanji // Kanji returned by the service
            {
                Id = 1,
                Literal = literal,
                Meanings = meanings,
                KunReadings = kunReadings,
                OnReadings = onReadings,
                NanoriReadings = nanoriReadings,
                Grade = grade,
                JLPTLevel = jlptLevel,
                StrokeCount = strokeCount,
                Frequency = frequency
            };

            mockService
                .Setup(service => service.GetKanjiByLiteralAsync(literal))
                .ReturnsAsync(kanji);

            var controller = new KanjiController(mockService.Object);
            var result = await controller.GetByLiteral(literal);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedKanji = Assert.IsType<Kanji>(okResult.Value);
            Assert.Equal(literal, returnedKanji.Literal);
            Assert.Equal(meanings, returnedKanji.Meanings);
            Assert.Equal(kunReadings, returnedKanji.KunReadings);
            Assert.Equal(onReadings, returnedKanji.OnReadings);
            Assert.Equal(nanoriReadings, returnedKanji.NanoriReadings);
            Assert.Equal(grade, returnedKanji.Grade);
            Assert.Equal(jlptLevel, returnedKanji.JLPTLevel);
            Assert.Equal(strokeCount, returnedKanji.StrokeCount);
            Assert.Equal(frequency, returnedKanji.Frequency);

            mockService.Verify(service => service.GetKanjiByLiteralAsync(literal), Times.Once());
        }
    }
}
