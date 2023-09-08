namespace PulseSurveyV2.Test.Controllers;

using Microsoft.AspNetCore.Mvc;
using Moq;
using PulseSurveyV2.Controllers;
using PulseSurveyV2.Models;
using Threenine.Data;
using System.Threading.Tasks;
using Xunit;

public class UsersControllerTests
{
    [Fact]
    public async Task PostUser_GivenTheRequestIsValid_ThenItShouldCallRepositoryInsertAsync()
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
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<UserDTO>(actionResult.Value);
        Assert.Equivalent(userDto, returnValue);
    }
    
    // [Fact]
    // public async Task GetUsers_ReturnsListOfUsers()
    // {
    //     // Arrange
    //     var uow = new Mock<IUnitOfWork<UnifiedContext>>();
    //     var usersRepository = new Mock<IRepositoryAsync<User>>();
    //     uow.Setup(u => u.GetRepositoryAsync<User>())
    //         .Returns(usersRepository.Object);
    //     var controller = new UsersController(uow.Object);
    //     var usersList = new List<User> { new User { UserId = 1 }, new User { UserId = 2 } };
    //     usersRepository.Setup(r => r.GetListAsync()).ReturnsAsync(new List<User>(usersList, 2, 1));
    //
    //     // Act
    //     var result = await controller.GetUsers();
    //
    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    //     var model = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
    //     Assert.Equal(2, model.Count);
    // }
    //
    // [Fact]
    // public async Task GetUser_WithValidId_ReturnsUser()
    // {
    //     // Arrange
    //     var uow = new Mock<IUnitOfWork<UnifiedContext>>();
    //     var usersRepository = new Mock<IRepositoryAsync<User>>();
    //     uow.Setup(u => u.GetRepositoryAsync<User>())
    //         .Returns(usersRepository.Object);
    //
    //     var controller = new UsersController(uow.Object);
    //     var userId = 1;
    //     var user = new User { UserId = userId };
    //     usersRepository.Setup(r => r.SingleOrDefaultAsync(u => u.UserId == userId)).ReturnsAsync(user);
    //
    //     // Act
    //     var result = await controller.GetUser(userId);
    //
    //     // Assert
    //     Assert.IsType<User>(result.Value);
    //     Assert.Equal(userId, result.Value.UserId);
    // }

}
