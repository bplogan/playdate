using System.Threading.Tasks;
using PlayDate.Models.TokenAuth;
using PlayDate.Web.Controllers;
using Shouldly;
using Xunit;

namespace PlayDate.Web.Tests.Controllers
{
    public class HomeController_Tests: PlayDateWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}