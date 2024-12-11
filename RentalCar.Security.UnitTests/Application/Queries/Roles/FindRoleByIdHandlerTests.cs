using Microsoft.AspNetCore.Identity;
using Moq;
using RentalCar.Security.Application.Handlers.Roles;
using RentalCar.Security.Application.Queries.Request.Roles;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Queries.Roles
{
    public class FindRoleByIdHandlerTests
    {
        [Fact]
        public async void FindRoleById_Executed_return_FindRoleResponse()
        {
            // Arrange
            var role = new IdentityRole
            {
                Id = "IdRole",
                Name = "Name",
                NormalizedName = "Name"
            };

            var repositoryMock = new Mock<IRoleRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();
            
            var findRoleByIdHandler = new FindRoleByIdHandler(repositoryMock.Object, prometheusServiceMock.Object, loggerServiceMock.Object);

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(role);

            // Act
            var result = await findRoleByIdHandler.Handle(new FindRoleByIdRequest(It.IsAny<string>()), new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), new CancellationToken()));
        }
    }
}
