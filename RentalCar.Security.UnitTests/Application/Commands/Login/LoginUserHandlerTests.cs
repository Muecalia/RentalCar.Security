using Microsoft.AspNetCore.Identity;
using Moq;
using RentalCar.Security.Application.Commands.Request.Login;
using RentalCar.Security.Application.Handlers.Login;
using RentalCar.Security.Core.Entities;
using RentalCar.Security.Core.Repositories;
using RentalCar.Security.Core.Services;

namespace RentalCar.Security.UnitTests.Application.Commands.Login
{
    public class LoginAccountHandlerTests
    {
        [Fact]
        public async void LoginAccount_Executed_return_LoginAccountResponse()
        {
            try
            {
                // Arrange
                string role = "Admin";
                string token = "eggege1hjtyhn";

                var loginRequest = new LoginUserRequest
                {
                    Email = "account@email.com",
                    Password = "password"
                };

                var account = new ApplicationUser 
                {
                    Id = "IdAccount",
                    Name = "Name",
                    Email = "account@email.com",
                    IsActive = true,
                    PhoneNumber = "1234567890",
                    CreatedAt = DateTime.Now
                };

                var _jwtTokenServiceMock = new Mock<IJwtTokenService>();
                var _loggerServiceMock = new Mock<ILoggerService>();
                var _accountRepositoryMock = new Mock<IAccountRepository>();
                var _prometheusServiceMock = new Mock<IPrometheusService>();


                _accountRepositoryMock.Setup(repo => repo.FindByEmail(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(account);
                _accountRepositoryMock.Setup(repo => repo.GetRoles(It.IsAny<ApplicationUser>(), new CancellationToken())).ReturnsAsync(role);
                _accountRepositoryMock.Setup(repo => repo.SignInUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(SignInResult.Success);
                _jwtTokenServiceMock.Setup(service => service.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(token);

                var loginAccountHandler = new LoginUserHandler(_jwtTokenServiceMock.Object, _loggerServiceMock.Object, _accountRepositoryMock.Object, _prometheusServiceMock.Object);
                
                // Act
                var result = await loginAccountHandler.Handle(loginRequest, new CancellationToken());

                // Assert
                Assert.NotNull(result.Data);
                Assert.NotNull(result.Message);
                Assert.NotEmpty(result.Message);
                Assert.True(result.Succeeded);

                _accountRepositoryMock.Verify(repo => repo.FindByEmail(It.IsAny<string>(), new CancellationToken()));
                _accountRepositoryMock.Verify(repo => repo.GetRoles(account, new CancellationToken()));
                _accountRepositoryMock.Verify(repo => repo.SignInUser(It.IsAny<string>(), It.IsAny<string>()));
                _jwtTokenServiceMock.Verify(service => service.GenerateJwtToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            }
            catch (Exception e)
            {
                throw; // TODO handle exception
            }
        }
    }
}
