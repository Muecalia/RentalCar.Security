using RentalCar.Security.Core.Entities;

namespace RentalCar.Security.UnitTests.Core.Entities
{
    public class ApplicationUserTests
    {
        [Fact]
        public void ApplicationUser_Executed_ReturnSuccess() 
        {
            // Arrange
            var user = new ApplicationUser 
            {
                Name = "Test",
                Email = "test@email.com",
                IdUser = "IdUser",
                IsClient = true,
                UserName = "test@email.com"                
            };

            // Act


            // Assert
            Assert.NotNull(user.Name);
            Assert.NotEmpty(user.Name);
            Assert.NotNull(user.Email);
            Assert.NotEmpty(user.Email);

            Assert.Equal(user.CreatedAt.Date, DateTime.Now.Date);
        }
    }
}
