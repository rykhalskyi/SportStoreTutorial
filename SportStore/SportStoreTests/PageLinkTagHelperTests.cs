using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using NUnit.Framework;
using SportStore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStoreTests
{
    [TestFixture]
    public class PageLinkTagHelperTests
    {
        //TODO: Implement tests. page 223

        [Test]
        public void PageLink_CanGeneateLinks_True()
        {
            //Arrange

            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);

            var helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new SportStore.Models.ViewModels.PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            var context = new TagHelperContext(
                new TagHelperAttibutesList(),
                new Dictionary<object, object>(),
                "");

            var content = new Mock<TagHelperContent>();
            var output = new TagHelperOutput("div",
                new TagHelperAttibutesList(),
                (cache, encoder) => Task.FromResult(content.Object));

            //Act
            helper.Process(context, output);

            //Assert
            Assert.AreEqual(@"<a href=""Test/Page1"">1</a>"
                         + @"<a href=""Test/Page2"">2</a>"
                         + @"<a href=""Test/Page3"">3</a>",
                         output.Content.GetContent());

        }
    }
}
