using Microsoft.AspNetCore.Identity;
using Moq;
using RentalCar.Security.Application.Handlers.Roles;
using RentalCar.Security.Application.Queries.Request.Roles;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Queries.Roles
{
    public class FindAllRolesHandlerTests
    {
        [Fact]
        public async void FindAllRoles_Executed_Return_List_FindRoleResponse()
        {
            // Arrange
            var roles = new List<IdentityRole>
            {
                new() { Id = "Id 1", Name = "Name 1", NormalizedName = "NAME 1" },
                new() { Id = "Id 2", Name = "Name 2", NormalizedName = "NAME 2" },
                new() { Id = "Id 3", Name = "Name 3", NormalizedName = "NAME 3" }
            };

            var repositoryMock = new Mock<IRoleRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();
            
            var findAllRolesHandler = new FindAllRolesHandler(repositoryMock.Object, prometheusServiceMock.Object, loggerServiceMock.Object);

            repositoryMock.Setup(repo => repo.GetAll(new CancellationToken())).ReturnsAsync(roles);

            // Act
            var result = await findAllRolesHandler.Handle(new FindAllRolesRequest(), new CancellationToken());

            // Assert
            Assert.NotNull(result.Datas);
            Assert.True(result.Succeeded);
            Assert.Equal(roles.Count, result.Datas.Count);

            repositoryMock.Verify(repo => repo.GetAll(new CancellationToken()));
        }
    }
}
