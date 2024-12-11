using Microsoft.AspNetCore.Identity;
using Moq;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Handlers.Roles;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Roles
{
    public class CreateRoleHandlerTests
    {
        [Fact]
        public async void CreateRole_Executed_ReturnInputRoleResponse()
        {
            //Arrange
            var createRoleRequest = new CreateRoleRequest 
            {
                Name = "Perfil 1"
            };

            var role = new IdentityRole 
            {
                Id = "IdRole",
                Name = "Name",
                NormalizedName = "Name"
            };

            var repositoryMock = new Mock<IRoleRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            repositoryMock.Setup(repo => repo.Exists(createRoleRequest.Name, new CancellationToken())).ReturnsAsync(false);
            repositoryMock.Setup(repo => repo.Create(createRoleRequest.Name)).ReturnsAsync(role);
            
            var createRoleHandler = new CreateRoleHandler(repositoryMock.Object, prometheusServiceMock.Object, loggerServiceMock.Object);

            // Act
            var result = await createRoleHandler.Handle(createRoleRequest, new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.Create(It.IsAny<string>()), Times.Once);
        }
    }
}
