using Bunit;
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
    public class BInputLabelTests : TestBase
    {
        [TestMethod]
        public void RenderWithLabelContent()
        {
            // Arrange
            var mock = new Mock<IInput>();
            mock.Setup(r => r.HasLabel).Returns(true);
            mock.Setup(r => r.LabelContent).Returns(r => r.AddContent(0, "Hello world!"));
            mock.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());

            // Act
            var cut = RenderComponent<BInputLabel<IInput>>(props =>
            {
                props
                    .Add(p => p.HasProviderComponent, mock.Object);
            });
            var component = cut.FindComponent<EmptyComponent>();
            var attrs = component.Instance.Attrs;

            // Assert
            Assert.AreEqual(1, attrs.Count);
            Assert.IsTrue(attrs.ContainsKey("ChildContent"));
        }
    }
}
