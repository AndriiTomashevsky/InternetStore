using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using InternetStore.Controllers;
using InternetStore.Models.Identity;
using Xunit;

namespace InternetStore.Tests
{
    public class FakeUserManager : UserManager<AppUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<AppUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<AppUser>>().Object,
                  new IUserValidator<AppUser>[0],
                  new IPasswordValidator<AppUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<AppUser>>>().Object)
        {
        }
    }

    public class FakeRoleManager : RoleManager<AppRole>
    {
        public FakeRoleManager()
            : base(new Mock<IRoleStore<AppRole>>().Object,
                  new IRoleValidator<AppRole>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<ILogger<RoleManager<AppRole>>>().Object)
        {
        }
    }

    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Users()
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(m => m.Users).Returns(new AppUser[] {
                //new AppUser { Id = "1" },
                //new AppUser { Id = "2", RoleId="A" },
                //new AppUser { Id = "3", RoleId="B" },
                //new AppUser { Id = "4", RoleId="A" },
                //new AppUser { Id = "5", RoleId="C" },
            }.AsQueryable);

            var mockRoleManager = new Mock<FakeRoleManager>();
            mockRoleManager.Setup(m => m.Roles).Returns(new AppRole[] {
                new AppRole { Id = "A" },
                new AppRole { Id = "B" },
                new AppRole { Id = "C" },
            }.AsQueryable);

            AdminController target =
                new AdminController(mockUserManager.Object, mockRoleManager.Object, null, null, null);

            //Act
            AppUser[] result = (target.Index().Model as IEnumerable<AppUser>).ToArray();

            //Assert
            Assert.Equal(5, result.Count());
            Assert.True(result[0].Id == "1");
            Assert.True(result[4].Id == "5");
        }
    }
}
