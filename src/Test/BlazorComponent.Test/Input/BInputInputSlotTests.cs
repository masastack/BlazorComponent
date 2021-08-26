using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Test.Input
{
    [TestClass]
    public class BInputInputSlotTests : TestBase
    {
        [TestMethod]
        public void ClickDivHandleOnClickShouldBeCalled()
        {
            // Arrange
            var mock = new Mock<IInput>();
            mock.Setup(r => r.HandleOnClick(It.IsAny<MouseEventArgs>())).Returns(Task.CompletedTask);
            mock.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());
            mock.Setup(r => r.CssProvider).Returns(new ComponentCssProvider());

            var cut = RenderComponent<BInputInputSlot<IInput>>(props =>
            {
                props
                    .Add(p => p.HasProviderComponent, mock.Object);
            });
            var div = cut.Find("div:first-child");

            // Act
            div.Click();

            // Assert
            mock.Verify(r => r.HandleOnClick(It.IsAny<MouseEventArgs>()), Times.Once());
        }
    }
}
