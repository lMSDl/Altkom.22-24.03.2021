using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;

namespace ClassLibrary.Test
{
    public class NumbersHolderUnitTest
    {

        [Fact]
        public void NumbersHolder_DefaultConstructor_ResturnsEmptyArray()
        {
            //Arrange
            var numbersHolder = new NumbersHolder();

            //Act
            var result = numbersHolder.Fetch();

            //Assert
            Assert.Empty(result);
            result.Should().BeEmpty();
        }

        [Fact]
        public void Add_SingleNumber_ResultsSameSingleNumber()
        {
            //Arrange
            var holder = new NumbersHolder();
            var number = It.IsAny<int>();

            //Act
            holder.Add(number);
            var result = holder.Fetch();

            //Assert
            Assert.Equal(new[] { number }, result);
            Assert.Single(result);
            Assert.Single(result, number);

            result.Should().ContainSingle().And.Contain(number);
        }

        [Fact]
        public async Task RemoveAsync_RemoveSingleNumber_CountZero()
        {
            //Arrange
            var holder = new NumbersHolder();
            holder.Add(0);

            //Act
            await holder.RemoveAsync();
            var result = holder.Count;

            //Assert
            Assert.Equal(0, result);
            result.Should().Be(0);
        }

        [Fact]
        public void RemoveAsync_FromEmpty_ThrowsInvalidOperationException()
        {
            //Arrange
            var holder = new NumbersHolder();

            //Act
            Func<Task> func = () => holder.RemoveAsync();
            Task Act() => holder.RemoveAsync();

            //Assert
            //Assert.ThrowsAsync<InvalidOperationException>(func);
            Assert.ThrowsAsync<InvalidOperationException>(Act);

            func.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
