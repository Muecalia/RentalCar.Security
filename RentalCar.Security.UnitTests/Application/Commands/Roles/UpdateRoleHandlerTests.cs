using Microsoft.AspNetCore.Identity;
using Moq;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Handlers.Roles;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Roles
{
    public class UpdateRoleHandlerTests
    {
        [Fact]
        public async void UpdateRole_Executed_ReturnInputRoleResponse()
        {
            // Arrange
            var updateRoleRequest = new UpdateRoleRequest 
            {
                Id = "IdRole",
                Name = "Name"
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

            var updateRoleHandler = new UpdateRoleHandler(repositoryMock.Object, prometheusServiceMock.Object, loggerServiceMock.Object);

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(role);
            repositoryMock.Setup(repo => repo.Update(role)).ReturnsAsync(true);

            // Act
            var result = await updateRoleHandler.Handle(updateRoleRequest, new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.True(result.Succeeded);

            repositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), new CancellationToken()));
            repositoryMock.Verify(repo => repo.Update(It.IsAny<IdentityRole>()));
        }
    }
}
