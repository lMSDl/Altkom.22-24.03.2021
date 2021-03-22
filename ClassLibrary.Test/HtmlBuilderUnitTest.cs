using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ClassLibrary.Test
{
    public class HtmlBuilderUnitTest
    {

        [Fact]
        public void Append_ResultsEqualToInput_WhenPassingSomeText()
        {
            //Arrange
            var htmlBuilder = new HtmlBuilder();

            //Act
            htmlBuilder.Append("a");
            var result = htmlBuilder.ToString();

            //Assert.Equal("a", result);
            result.Should().Be("a");
        }

        [Fact]
        public void AppendBold_ResultsBoldInput_WhenPassingSomeText()
        {
            //Arrange
            var htmlBuilder = new HtmlBuilder();
            var input = "a";

            //Act
            htmlBuilder.AppendBold(input);
            var result = htmlBuilder.ToString();

            result.Should().StartWith("<b>").And.Contain(input).And.EndWith("</b>");

            //Assert.StartsWith("<b>", result);
            //Assert.Contains(input, result);
            //Assert.EndsWith("</b>", result);
        }

        [Fact]
        public void AppendAnchor_ResultsHref_WhenPassingUrlAndLabel()
        {
            //Arrange
            var htmlBuilder = new HtmlBuilder();
            var href = "http://abc.com";
            var label = "label";

            //Act
            htmlBuilder.AppendAnchor(href, label);
            var result = htmlBuilder.ToString();


            result.Should().StartWith("<a href=").And.Contain(href, label).And.EndWith("</a>");

            /*Assert.StartsWith("<a href=", result);
            Assert.Contains(href, result);
            Assert.Contains(label, result);
            Assert.EndsWith("</a>", result);*/
        }

        [Fact]
        public void AppendAnchor_ThrowsInvalidOperationException_WhenPassingBadUrl()
        {
            //Arrange
            var htmlBuilder = new HtmlBuilder();
            var href = "abc";
            var label = "label";

            //Act
            Action action = () => htmlBuilder.AppendAnchor(href, label);

            //Assert
            htmlBuilder.Invoking(x => x.AppendAnchor(href, label)).Should().Throw<InvalidOperationException>();
            action.Should().Throw<InvalidOperationException>();

            Assert.Throws<InvalidOperationException>(action);
        }
    }
}
