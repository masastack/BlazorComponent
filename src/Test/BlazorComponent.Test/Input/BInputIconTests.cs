using Bunit;
using Microsoft.AspNetCore.Components;
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
    public class BInputIconTests : TestBase
    {
        [TestMethod]
        public void RenderWithoutIconShouldThrowException()
        {
            // Arrange
            var mock = new Mock<IInput>();
            mock.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());
            mock.Setup(r => r.CssProvider).Returns(new ComponentCssProvider());

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => RenderComponent<BInputIcon<IInput>>(props =>
            {
                props.Add(p => p.AbstractComponent, mock.Object);
            }));
        }

        [TestMethod]
        public void RenderWithoutTypeShouldThrowException()
        {
            // Arrange
            var mock = new Mock<IInput>();
            mock.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());
            mock.Setup(r => r.CssProvider).Returns(new ComponentCssProvider());

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => RenderComponent<BInputIcon<IInput>>(props =>
            {
                props.Add(p => p.AbstractComponent, mock.Object);
                props.Add(p => p.Icon, "mdi-clear");
            }));
        }

        [TestMethod]
        public void RenderWithIconAndType()
        {
            // Arrange
            var mock = new Mock<IInput>();
            mock.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());

            var cssProvider = new ComponentCssProvider();
            cssProvider
                .Apply("test-type", cssBuilder =>
                {
                    cssBuilder
                        .Add("test-class");
                });
            mock.Setup(r => r.CssProvider).Returns(cssProvider);

            // Act
            var cut = RenderComponent<BInputIcon<IInput>>(props =>
             {
                 props.Add(p => p.AbstractComponent, mock.Object);
                 props.Add(p => p.Icon, "mdi-clear");
                 props.Add(p => p.Type, "test-type");
             });
            var component = cut.FindComponent<EmptyComponent>();
            var attrs = component.Instance.Attrs;

            // Assert
            cut.MarkupMatches(@"<div class=""test-class""></div>");
            Assert.AreEqual(2, attrs.Count);
            Assert.IsTrue(attrs.ContainsKey("ChildContent"));
            Assert.IsTrue(attrs.ContainsKey("OnClick"));
        }

        [TestMethod]
        public void RenderWithOnClickShouldBePassedToTargetComponent()
        {
            // Arrange
            var input = new Mock<IInput>();
            input.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());
            input.Setup(r => r.CssProvider).Returns(new ComponentCssProvider());

            var receiver = new Mock<IHandleEvent>();
            receiver.Setup(r => r.HandleEventAsync(It.IsAny<EventCallbackWorkItem>(), It.IsAny<object?>())).Returns(Task.CompletedTask);

            var cut = RenderComponent<BInputIcon<IInput>>(props =>
            {
                props.Add(p => p.AbstractComponent, input.Object);
                props.Add(p => p.Icon, "mdi-clear");
                props.Add(p => p.Type, "test-type");
                props.Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(receiver.Object, () => { }));
            });

            // Act
            var component = cut.FindComponent<EmptyComponent>();
            var attrs = component.Instance.Attrs;

            // Assert
            Assert.IsTrue(attrs.ContainsKey("OnClick"));
            if (attrs["OnClick"] is EventCallback<MouseEventArgs> callback)
            {
                Assert.IsTrue(callback.HasDelegate);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
