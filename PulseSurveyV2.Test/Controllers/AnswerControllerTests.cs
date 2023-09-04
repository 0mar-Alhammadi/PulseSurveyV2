namespace PulseSurveyV2.Test.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Controllers;
using PulseSurveyV2.Models;
using Xunit;
using Xunit.Abstractions;

public class AnswerControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AnswerControllerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
}
