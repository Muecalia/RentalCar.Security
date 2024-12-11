using Moq;
using RentalCar.Security.Application.Handlers.Account;
using RentalCar.Security.Application.Queries.Request.Account;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Queries.Account
{
    public class FindAllAccountsHandlerTests
    {
        [Fact]
        public async void FindAllAccounts_Executed_Return_List_FindAllAccountsResponse()
        {
            // Arrange
            var accounts = new List<ApplicationUser>
            {
                new ApplicationUser{ Id = "Id 1", Name = "Name 1", Email = "email 1", CreatedAt = DateTime.Now, PhoneNumber = "phone 1" },
                new ApplicationUser{ Id = "Id 2", Name = "Name 2", Email = "email 2", CreatedAt = DateTime.Now, PhoneNumber = "phone 2" },
                new ApplicationUser{ Id = "Id 3", Name = "Name 3", Email = "email 3", CreatedAt = DateTime.Now, PhoneNumber = "phone 3" },
                new ApplicationUser{ Id = "Id 4", Name = "Name 4", Email = "email 4", CreatedAt = DateTime.Now, PhoneNumber = "phone 4" }
            };

            var repositoryMock = new Mock<IAccountRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            repositoryMock.Setup(repo => repo.FindAll(new CancellationToken())).ReturnsAsync(accounts);

            var findAllAccountsHandler = new FindAllAccountsHandler(repositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await findAllAccountsHandler.Handle(new FindAllAccountsRequest(), new CancellationToken());

            // Assert
            Assert.NotNull(result.Datas);
            Assert.Equal(accounts.Count, result.Datas.Count);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.FindAll(new CancellationToken()), Times.Once);
        }
    }
}
