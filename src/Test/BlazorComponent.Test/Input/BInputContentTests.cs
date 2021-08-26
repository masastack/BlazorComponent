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
    public class BInputContentTests : TestBase
    {
        [TestMethod]
        public void RenderShouldBe3AbstractComponent()
        {
            // Arrange
            var mock = new Mock<IInput>();
            mock.Setup(r => r.AbstractProvider).Returns(new ComponentAbstractProvider());

            // Act
            var cut = RenderComponent<BInputContent<IInput>>(props =>
            {
                props
                    .Add(p => p.HasProviderComponent, mock.Object);
            });

            // Assert
            cut.MarkupMatches("");
            var abstracts = cut.FindComponents<AbstractComponent>();
            Assert.AreEqual(3, abstracts.Count);
        }
    }
}
