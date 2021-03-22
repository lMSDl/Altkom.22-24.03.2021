using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

            Assert.Equal("a", result);
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

            Assert.StartsWith("<b>", result);
            Assert.Contains(input, result);
            Assert.EndsWith("</b>", result);
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

            Assert.StartsWith("<a href=", result);
            Assert.Contains(href, result);
            Assert.Contains(label, result);
            Assert.EndsWith("</a>", result);
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
            Assert.Throws<InvalidOperationException>(action);
        }
    }
}
