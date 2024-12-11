using Moq;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Application.Queries.Request.Account;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Queries.Account
{
    public class FindAccountByIdHandlerTests
    {
        [Fact]
        public async void FindAccountById_Executed_Return_FindAccountResponse()
        {
            // Arrange
            var account = new ApplicationUser
            { 
                Id = "Id", 
                Name = "Name", 
                Email = "email",
                CreatedAt = DateTime.Now,
                PhoneNumber = "phone" 
            };

            var roles = "Admin";

            var repositoryMock = new Mock<IAccountRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            repositoryMock.Setup(repo => repo.FindById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(account);
            repositoryMock.Setup(repo => repo.GetRoles(It.IsAny<ApplicationUser>(), new CancellationToken())).ReturnsAsync(roles);

            var findAccountByIdHandler = new FindAccountByIdHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await findAccountByIdHandler.Handle(new FindAccountByIdRequest("id"), new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.FindById(It.IsAny<string>(), new CancellationToken()));
            repositoryMock.Verify(repo => repo.GetRoles(It.IsAny<ApplicationUser>(), new CancellationToken()));

        }
    }
}
