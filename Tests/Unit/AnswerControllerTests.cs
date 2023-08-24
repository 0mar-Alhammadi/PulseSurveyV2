using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Controllers;
using PulseSurveyV2.Models;

namespace PulseSurveyV2.Tests.Unit;

public class AnswerControllerTests
{
    [Fact]
    public async Task PostAnswer_ValidInput_ReturnsCreatedAtAction()
    {
        // Arrange
        var answerDto = new AnswerDTO
        {
            SurveyId = 1,
            UserId = 1,
            AnswerRating = 5
        };

        var mockContext = new Mock<UnifiedContext>();
        mockContext.Setup(c => c.Answers).Returns(Mock.Of<DbSet<Answer>>());

        var controller = new AnswersController(mockContext.Object);

        // Act
        var result = await controller.PostAnswer(answerDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Answer>(createdAtActionResult.Value);

        Assert.Equal(answerDto.SurveyId, returnValue.SurveyId);
        Assert.Equal(answerDto.UserId, returnValue.UserId);
        Assert.Equal(answerDto.AnswerRating, returnValue.AnswerRating);

    }
    
     [Fact]
        public async Task GetAnswer_ExistingId_ReturnsAnswer()
        {
            // Arrange
            var existingAnswer = new Answer { AnswerId = 1, SurveyId = 1, UserId = 1, AnswerRating = 5 };
            
            var mockDbSet = new Mock<DbSet<Answer>>();
            mockDbSet.Setup(dbSet => dbSet.FindAsync(It.IsAny<long>()))
                     .ReturnsAsync((long id) => existingAnswer.AnswerId == id ? existingAnswer : null);

            var mockContext = new Mock<UnifiedContext>();
            mockContext.Setup(context => context.Answers).Returns(mockDbSet.Object);

            var controller = new AnswersController(mockContext.Object);

            // Act
            var result = await controller.GetAnswer(existingAnswer.AnswerId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Answer>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Answer>(okResult.Value);

            Assert.Equal(existingAnswer.SurveyId, returnValue.SurveyId);
            Assert.Equal(existingAnswer.UserId, returnValue.UserId);
            Assert.Equal(existingAnswer.AnswerRating, returnValue.AnswerRating);
            // ... Add more assertions if needed
        }

        [Fact]
        public async Task GetAnswer_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Answer>>();
            mockDbSet.Setup(dbSet => dbSet.FindAsync(It.IsAny<long>()))
                     .ReturnsAsync((long id) => null);

            var mockContext = new Mock<UnifiedContext>();
            mockContext.Setup(context => context.Answers).Returns(mockDbSet.Object);

            var controller = new AnswersController(mockContext.Object);

            // Act
            var result = await controller.GetAnswer(123); // Assuming 123 is a non-existing ID

            // Assert
            var actionResult = Assert.IsType<ActionResult<Answer>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
}
