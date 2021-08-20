using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Test.Core.Abstract
{
    [TestClass]
    public class AbstractComponentTests : TestBase
    {
        [TestMethod]
        public void RenderWithoutMetadataShouldThrowException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => RenderComponent<AbstractComponent>());
        }

        [TestMethod]
        public void RenderHelloComponent()
        {
            // Act
            var cut = RenderComponent<AbstractComponent>(props =>
            {
                props.Add(p => p.Metadata, new AbstractMetadata(typeof(HelloComponent)));
            });

            // Assert
            cut.HasComponent<HelloComponent>();
        }

        [TestMethod]
        public void RenderHelloComponentWithMessage()
        {
            // Act
            var cut = RenderComponent<AbstractComponent>(props =>
            {
                props.Add(p => p.Metadata, new AbstractMetadata(typeof(HelloComponent), new Dictionary<string, object> {
                    {"Message","Hello world!"}
                }));
            });

            // Assert
            cut.MarkupMatches("<div>Hello world!</div>");
        }
    }
}
