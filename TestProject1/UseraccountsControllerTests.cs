using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VodilaASPApp.Controllers;
using VodilaASPApp.Models;
using Xunit;

namespace TestProject1
{
    public class UseraccountsControllerTests
    {
        UseraccountsController controller;
        public UseraccountsControllerTests()
        {
            controller = new UseraccountsController(new VodilaASPApp.Models.VodilaContext());
        }

        [Fact]
        public async Task DetailsResultIsNotFound()
        {
            
            var result = await controller.Details(-1);
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task IndexModelIsListAndNotEmpty()
        {
            var result = Assert.IsType<ViewResult>(await controller.Index());

            var list = (List<Useraccount>?)result.Model;

            Assert.NotEmpty(list);
        }
    }
}