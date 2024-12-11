using Microsoft.AspNetCore.Identity;
using Moq;
using RentalCar.Security.Application.Commands.Request.Roles;
using RentalCar.Security.Application.Handlers.Roles;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Roles
{
    public class DeleteRoleHandlerTests
    {
        [Fact]
        public async void DeleteRole_Executed_ReturnInputRoleResponse() 
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
            
            var deleteRoleHandler = new DeleteRoleHandler(repositoryMock.Object, prometheusServiceMock.Object, loggerServiceMock.Object);

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(role);
            repositoryMock.Setup(repo => repo.Delete(role)).ReturnsAsync(true);

            // Act
            var result = await deleteRoleHandler.Handle(new DeleteRoleRequest(It.IsAny<string>()), new CancellationToken());

            // Assert
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Message);
            Assert.NotEmpty(result.Message);

            repositoryMock.Verify(repo => repo.Delete(It.IsAny<IdentityRole>()));
        }
    }
}
