using Moq;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Account
{
    public class ChangePasswordHandlerTests
    {
        [Fact]
        public async void ChangePasswordUser_Executed_Return_InputUserResponse()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            var changePasswordUserRequest = new ChangePasswordRequest 
            {
                Id = "Id",
                NewPassword = "NewPassword",
                OldPassword = "OldPassword"
            };

            var user = new ApplicationUser
            {
                Id = "IdUser",
                Name = "Name",
                Email = "user@email.com",
                IsActive = true,
                PhoneNumber = "1234567890",
                CreatedAt = DateTime.Now
            };

            repositoryMock.Setup(repo => repo.FindById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(user);
            repositoryMock.Setup(repo => repo.ChangePassword(user, It.IsAny<string>(), It.IsAny<string>(), new CancellationToken()));

            var changePasswordUserHandler = new ChangePasswordHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await changePasswordUserHandler.Handle(changePasswordUserRequest, new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.FindById(It.IsAny<string>(), new CancellationToken()), Times.Once());
            repositoryMock.Verify(repo => repo.ChangePassword(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>(), new CancellationToken()), Times.Once());
        }
    }
}
