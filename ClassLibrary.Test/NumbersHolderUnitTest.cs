using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using FluentAssertions.Execution;
using Bogus;

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
            //numbersHolder.Add(0);
            var result = numbersHolder.Fetch();

            //Assert
            //Assert.Empty(result);
            result.Should().BeEmpty("because we do not put anything there");
        }

        [Fact]
        public void Add_SingleNumber_ResultsSameSingleNumber()
        {
            //Arrange
            var holder = new NumbersHolder();
            int number = default;

            //Act
            holder.Add(number);
            //holder.Add(3);
            var result = holder.Fetch();

            //Assert
               // Assert.Equal(new[] { number }, result);
               // Assert.Single(result);
               // Assert.Single(result, number);
        
            using (new AssertionScope())
            {
                //5.Should().Be(-5);
                result.Should().ContainSingle().And.Contain(number);
            }
        }

        [Fact]
        public async Task RemoveAsync_RemoveSingleNumber_CountZero()
        {
            //Arrange
            var value = new Faker().Random.Number();
            var holder = new NumbersHolder();
            holder.Add(value);

            //Act
            await holder.RemoveAsync();
            var result = holder.Count;

            //Assert
            Assert.Equal(value, result);
            result.Should().Be(value);
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

            func.Should().ThrowAsync<InvalidOperationException>().WithMessage("message");
        }
    }
}
