using Microsoft.AspNetCore.Mvc;
using Moq;
using PulseSurveyV2.Controllers;
using PulseSurveyV2.Models;
using Threenine.Data;
using System.Threading.Tasks;
using Xunit;

namespace PulseSurveyV2.Test.Controllers;

public class SurveysControllerTests
{
    [Fact]
    public async Task PostSurvey_GivenTheRequestIsValid_ThenItShouldCallRepositoryInsertAsync()
    {
        // Arrange
        var uow = new Mock<IUnitOfWork<UnifiedContext>>();
        var surveyRepo = new Mock<IRepositoryAsync<Survey>>();
        var controller = new SurveysController(uow.Object);
        var surveyDto = new SurveyDTO
        {
            UserId = 1,
            SurveyTitle = "TestSurvey",
            SurveyQuestion = "TestQuestion"
        };

        uow.Setup(u => u.GetRepositoryAsync<Survey>())
            .Returns(surveyRepo.Object)
            .Verifiable();

        surveyRepo.Setup(u => u.InsertAsync(It.IsAny<Survey>(), It.IsAny<CancellationToken>()))
            .Callback<Survey, CancellationToken>(
                (entity, cancellationToken) => { entity.SurveyId = 1; }
            );

        uow.Setup(u => u.CommitAsync())
            .Verifiable();

        // Act
        var result = await controller.PostSurvey(surveyDto);

        // Assert
        uow.Verify();
        surveyRepo.Verify(
            x => x.InsertAsync(
                It.Is<Survey>(u => u.SurveyTitle == "TestSurvey" && u.SurveyQuestion == "TestQuestion" && u.UserId == 1),
                It.IsAny<CancellationToken>()
            )
        );

        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<SurveyDTO>(actionResult.Value);
        Assert.Equivalent(surveyDto, returnValue);
    }
}