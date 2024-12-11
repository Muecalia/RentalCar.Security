using Moq;
using RentalCar.Security.Application.Commands.Request.Account;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Account
{
    public class CreateAccountHandlerTests
    {
        [Fact]
        public async void CreateAccount_Executed_Return_InputAccountResponse()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            var createAccountRequest = new CreateAccountRequest
            {
                Name = "name",
                Phone = "phone",
                Email = "email",
                Role = "role",
                IdUser = "IdUser",
                IsClient = true,
                Password = "password"
            };

            var account = new ApplicationUser
            {
                Id = "IdAccount",
                Name = "Name",
                Email = "account@email.com",
                PhoneNumber = "1234567890",
                CreatedAt = DateTime.Now
            };

            repositoryMock.Setup(repo => repo.IsExists(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(false);
            repositoryMock.Setup(repo => repo.IsEmailExists(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(false);
            repositoryMock.Setup(repo => repo.Create(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>(), new CancellationToken())).ReturnsAsync(account);

            var createAccountHandler = new CreateAccountHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await createAccountHandler.Handle(createAccountRequest, new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.IsExists(It.IsAny<string>(), new CancellationToken()));
            repositoryMock.Verify(repo => repo.IsEmailExists(It.IsAny<string>(), new CancellationToken()));
            repositoryMock.Verify(repo => repo.Create(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>(), new CancellationToken()));
        }
    }
}
