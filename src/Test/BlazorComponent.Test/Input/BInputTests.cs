using BlazorComponent.Test.Input;
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
            cut.HasComponent<AbstractComponent>();
        }

        [TestMethod]
        public void RenderShowDetailsShouldBeTrue()
        {
            // Act
            var cut = RenderComponent<TestInput>();

            // Assert
            Assert.IsTrue(cut.Instance.ShowDetails);
        }

        [TestMethod]
        public void RenderHideDetailsShowDetailsShouldBeTrue()
        {
            // Act
            var cut = RenderComponent<TestInput>(props=> {
                props.Add(p=>p.HideDetails,true);
            });

            // Assert
            Assert.IsFalse(cut.Instance.ShowDetails);
        }
    }
}