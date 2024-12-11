using Moq;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Account
{
    public class DeleteAccountHandlerTests
    {
        [Fact]
        public async void DeleteAccount_Executed_Return_InputAccountResponse()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            var account = new ApplicationUser
            {
                Id = "Id",
                Name = "Name",
                Email = "account@email.com",
                PhoneNumber = "1234567890",
                CreatedAt = DateTime.Now
            };

            repositoryMock.Setup(repo => repo.FindById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(account);
            repositoryMock.Setup(repo => repo.Delete(It.IsAny<ApplicationUser>(), new CancellationToken()));

            var deleteAccountHandler = new DeleteAccountHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await deleteAccountHandler.Handle(new DeleteAccountRequest("id"), new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.FindById(It.IsAny<string>(), new CancellationToken()), Times.Once);
            repositoryMock.Verify(repo => repo.Delete(It.IsAny<ApplicationUser>(), new CancellationToken()), Times.Once);

        }
    }
}
