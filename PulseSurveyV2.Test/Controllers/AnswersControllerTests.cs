namespace PulseSurveyV2.Test.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Controllers;
using PulseSurveyV2.Models;
using Xunit;
using Threenine.Data;
using Moq;
using Xunit.Abstractions;

public class AnswersControllerTests
{
    [Fact] 
    public async Task PostAnswer_GivenTheRequestIsValid_ThenItShouldCallRepositoryInsertAsync()
    {
        // Arrange
            var uow = new Mock<IUnitOfWork<UnifiedContext>>();
            var answerRepo = new Mock<IRepositoryAsync<Answer>>();
            var controller = new AnswersController(uow.Object);
            var answerDto = new AnswerDTO
            {
                SurveyId = 1,
                UserId = 1,
                AnswerRating = 5
            };

            uow.Setup(u => u.GetRepositoryAsync<Answer>())
                .Returns(answerRepo.Object)
                .Verifiable();

            answerRepo.Setup(u => u.InsertAsync(It.IsAny<Answer>(), It.IsAny<CancellationToken>()))
                .Callback<Answer, CancellationToken>(
                    (entity, cancellationToken) => { entity.AnswerId = 1; }
                );

            uow.Setup(u => u.CommitAsync())
                .Verifiable();

            // Act
            var result = await controller.PostAnswer(answerDto);

            // Assert
            uow.Verify();
            answerRepo.Verify(
                x => x.InsertAsync(
                    It.Is<Answer>(u => u.SurveyId == 1 && u.UserId == 1 && u.AnswerRating == 5),
                    It.IsAny<CancellationToken>()
                )
            );

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<AnswerDTO>(actionResult.Value);
            Assert.Equivalent(answerDto, returnValue);
        }
    }
        
    
    // private readonly ITestOutputHelper _testOutputHelper;
    //
    // public AnswersControllerTests(ITestOutputHelper testOutputHelper)
    // {
    //     _testOutputHelper = testOutputHelper;
    // }

    // [Fact]
    // public async Task PostAnswer_ValidInput_ReturnsCreatedAtAction()
    // {
    //     // Arrange
    //     var options = new DbContextOptionsBuilder<UnifiedContext>()
    //         .UseInMemoryDatabase(databaseName: "TestDb")
    //         .Options;
    //     
    //         var controller = new AnswersController(context);
    //         var answerDto = new AnswerDTO
    //         {
    //             SurveyId = 1,
    //             UserId = 1,
    //             AnswerRating = 5
    //         };
    //
    //         // Act
    //         var result = await controller.PostAnswer(answerDto);
    //
    //         // Assert
    //         var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    //         var returnValue = Assert.IsType<Answer>(createdAtActionResult.Value);
    //
    //         Assert.Equal(1, returnValue.SurveyId);
    //         Assert.Equal(1, returnValue.UserId);
    //         Assert.Equal(5, returnValue.AnswerRating);
    // }



    // [Fact]
    // public async Task GetAnswer_ExistingId_ReturnsAnswer()
    // {
    //     // Arrange
    //     var options = new DbContextOptionsBuilder<UnifiedContext>()
    //         .UseInMemoryDatabase(databaseName: "TestDb")
    //         .Options;
    //
    //     using (var context = new UnifiedContext(options))
    //     {
    //
    //         var controller = new AnswersController(context);
    //         
    //         var existingAnswer = new Answer { AnswerId = 1, SurveyId = 1, UserId = 1, AnswerRating = 5 };
    //         context.Answers.Add(existingAnswer);
    //         await context.SaveChangesAsync();
    //         
    //         // Act
    //         var result = await controller.GetAnswer(existingAnswer.AnswerId);
    //         _testOutputHelper.WriteLine(result.ToString());
    //
    //         // Assert
    //         var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    //         var returnValue = Assert.IsType<Answer>(createdAtActionResult.Value);
    //
    //         Assert.Equal(existingAnswer.SurveyId, returnValue.SurveyId);
    //         Assert.Equal(existingAnswer.UserId, returnValue.UserId);
    //         Assert.Equal(existingAnswer.AnswerRating, returnValue.AnswerRating);
    //     }
    // }
