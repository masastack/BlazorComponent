using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BlazorComponent.Test
{
    [TestClass]
    public class BInputTests : TestBase
    {
        [TestMethod]
        public void RenderShouldBeADiv()
        {
            // Act
            var cut = RenderComponent<BInput>();

            // Assert
            cut.MarkupMatches(@"<div class="""" style="""" id:ignore></div>");
        }
    }
}