using Microsoft.AspNetCore.Identity;

namespace RentalCar.Security.UnitTests.Core.Entities
{
    public class IdentityRoleTests
    {
        [Fact]
        public void IdentityRole_Returned_Success()
        {
            // Arrange
            var role = new IdentityRole
            {
                Name = "Role"
            };

            // Act

            // Assert
            Assert.NotNull(role.Name);
            Assert.NotEmpty(role.Name);
        }
    }
}
