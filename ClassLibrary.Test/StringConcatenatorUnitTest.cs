using System;
using Xunit;

namespace ClassLibrary.Test
{
    [TestCaseOrderer("ClassLibrary.Test.Orderers.AlphabeticalOrderer", "ClassLibrary.Test")]
    public class StringConcatenatorUnitTest : IDisposable
    {
        public StringConcatenatorUnitTest()
        {
            _stringConcatenator = new StringConcatenator();
        }

        private readonly StringConcatenator  _stringConcatenator;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        private StringConcatenator CreateDefaultStringConcatenator()
        {
            return new StringConcatenator();
        }

        [Fact]
        public void Concat_ThrowsArgumentNullException_WhenArgumentIsNull()
        {
            //Arrage
            var stringConcatenator = CreateDefaultStringConcatenator();

            //Act
            Action action = () => stringConcatenator.Concat(null);

            //Assert
            var argumentNullException = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("value", argumentNullException.ParamName);
        }

        [Fact]
        public void Concat_ResultsSameStringAsInput_WhenPassingSingleSign()
        {
            //Arrage
            //var stringConcatenator = new StringConcatenator();

            //Act
            _stringConcatenator.Concat("0");
            var result = _stringConcatenator.ToString();

            //Assert
            Assert.Equal("0", result);
        }

        [Fact]
        public void Concat_ThrowsOverflowException_WhenPassingLongString()
        {
            //Arrage
            var stringConcatenator = new StringConcatenator();
            const string OVER_MAXIMUM_LENGTH = "ala ma kota i psa";

            //Act
            Action action = () => stringConcatenator.Concat(OVER_MAXIMUM_LENGTH);

            //Assert
            Assert.Throws<OverflowException>(action);
        }



        [Theory]
        [InlineData("a", "a")]
        [InlineData("A", "a")]
        [InlineData("AbC", "abc")]
        public void Concat_RestursExpected_WhenPassingStrings(string input, string expected)
        {
            //Arrage
            var stringConcatenator = new StringConcatenator();

            //Act
            stringConcatenator.Concat(input);
            var result = stringConcatenator.ToString();

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
