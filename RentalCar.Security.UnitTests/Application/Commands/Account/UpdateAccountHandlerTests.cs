using Moq;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Account
{
    public class UpdateAccountHandlerTests
    {
        [Fact]
        public async void UpdateAccount_Executed_Return_InputAccountResponse()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            var updateAccountRequest = new UpdateAccountRequest
            {
                Id = "Id",
                Name = "name",
                Phone = "phone",
                Email = "email"
            };

            var account = new ApplicationUser
            {
                Id = "IdAccount",
                Name = "Name",
                Email = "account@email.com",
                PhoneNumber = "1234567890",
                CreatedAt = DateTime.Now
            };

            repositoryMock.Setup(repo => repo.FindById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(account);
            repositoryMock.Setup(repo => repo.FindByUser(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(account);
            repositoryMock.Setup(repo => repo.Update(It.IsAny<ApplicationUser>(), new CancellationToken()));

            var updateAccountHandler = new UpdateAccountHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await updateAccountHandler.Handle(updateAccountRequest, new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);
            Assert.True(result.Succeeded);
            
            repositoryMock.Verify(repo => repo.Update(It.IsAny<ApplicationUser>(), new CancellationToken()), Times.Once);
        }
    }
}
