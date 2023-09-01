namespace PulseSurveyV2.Test.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PulseSurveyV2.Controllers;
using PulseSurveyV2.Models;
using Threenine.Data;

public class UsersControllerTests
{
    [Fact]
    public async Task MethodName_GivenTheRequestIsValid_ThenItShouldCallRepositoryInsertAsync()
    {
        // Arrange
        var uow = new Mock<IUnitOfWork<UnifiedContext>>();
        var userRepo = new Mock<IRepositoryAsync<User>>();
        var controller = new UsersController(uow.Object);
        var userDto = new UserDTO
        {
            IsCreator = true,
            UserName = "TestUser",
        };

        uow.Setup(u => u.GetRepositoryAsync<User>())
            .Returns(userRepo.Object)
            .Verifiable();

        userRepo.Setup(u => u.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>(
                (entity, cancellationToken) => { entity.UserId = 1; }
            );

        uow.Setup(u => u.CommitAsync())
            .Verifiable();

        // Act
        var result = await controller.PostUser(userDto);

        // Assert
        uow.Verify();
        userRepo.Verify(
            x => x.InsertAsync(
                It.Is<User>(u => u.UserName == "TestUser" && u.IsCreator == true),
                It.IsAny<CancellationToken>()
            )
        );

        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<UserDTO>(actionResult.Value);
        Assert.Equivalent(userDto, returnValue);
    }
    
    [Fact]
    public async Task PostUser_GivenTheRequestIsInvalid_ThenItShouldReturnBadRequest()
    {
        // Arrange
        var uow = new Mock<IUnitOfWork<UnifiedContext>>();
        var userRepo = new Mock<IRepositoryAsync<User>>();
        var controller = new UsersController(uow.Object);
        var userDto = new UserDTO {};

        uow.Setup(u => u.GetRepositoryAsync<User>())
            .Returns(userRepo.Object)
            .Verifiable();
        
        uow.Setup(u => u.CommitAsync())
            .Verifiable();
        
        // Act
        var result = await controller.PostUser(userDto);

        // Assert
        uow.Verify();
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<UserDTO>(actionResult.Value);
        Assert.Equivalent(userDto, returnValue);
    }
}
