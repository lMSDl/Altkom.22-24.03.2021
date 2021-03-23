using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace ClassLibrary.Test
{
    public class LongExtensionsUnitTest
    {

        [Fact]
        public void ToDateTime_ResturnsDateTime_FromUnixTime()
        {
            var timestamp = 1616416571l;

            var result = timestamp.ToDateTime();

            Assert.Equal(new DateTime(2021, 3, 22, 12, 36, 11, DateTimeKind.Utc), result);

            result.Should().Be(22.March(2021).At(12, 36, 11).AsUtc());
            result.Should().BeCloseTo(22.March(2021).At(12, 36, 10).AsUtc(), 1000);
        }
    }
}
