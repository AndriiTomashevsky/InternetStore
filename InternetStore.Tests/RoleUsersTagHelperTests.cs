using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetStore.Infrastructure.TagHelpers;
using InternetStore.Models.Identity;
using Xunit;

namespace InternetStore.Tests
{
    public class RoleUsersTagHelperTests
    {
        [Fact]
        public void Can_Generate_User_Sequance()
        {
            //Arrange
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(m => m.Users).Returns(new AppUser[] {
                //new AppUser { Id = "1", UserName="Joe", RoleId="A" },
                //new AppUser { Id = "2", UserName="Alice", RoleId="A" },
                //new AppUser { Id = "3", UserName="Joel", RoleId="B" },
                //new AppUser { Id = "4", UserName="Bob", RoleId="A" },
                //new AppUser { Id = "5", UserName="Ethan", RoleId="C" },
            }.AsQueryable);

            var mockRoleManager = new Mock<FakeRoleManager>();
            mockRoleManager.Setup(m => m.Roles).Returns(new AppRole[] {
                new AppRole { Id = "A", Name="Admin" },
                new AppRole { Id = "B", Name="Manager" },
                new AppRole { Id = "C", Name="Employee" },
            }.AsQueryable);

            mockRoleManager.Setup(m => m.FindByIdAsync(It.Is<string>(v => v == "A")))
            .ReturnsAsync(mockRoleManager.Object.Roles.Where(r => r.Id == "A").FirstOrDefault());

            RoleUsersTagHelper target = new RoleUsersTagHelper(mockUserManager.Object, mockRoleManager.Object)
            {
                RoleId = "A"
            };

            TagHelperContext tagContext = new TagHelperContext(new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");

            TagHelperOutput output = new TagHelperOutput("", new TagHelperAttributeList(),
                (cach, encoder) => Task.FromResult(new Mock<TagHelperContent>().Object));

            //Act
            target.ProcessAsync(tagContext, output);

            //Arrange
            Assert.Equal("Joe, Alice, Bob", output.Content.GetContent());
        }
    }
}
