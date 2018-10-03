using System;
using Claimini.Shared.Extensions;
using FluentAssertions;
using Xunit;

namespace Claimini.Shared.Tests.Extensions
{
    public class LongExtensionsTests
    {
        [Fact]
        public void UnixTimeStampToDateTime_WithZeroUnixTimestamp_ShouldReturnDateTimeAtStartOfUnixEpochAndShouldBeUtc()
        {
            // Arrange
            var expectedDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long unixTimestamp = 0;
            
            // Act
            DateTime epochStartDateTime = unixTimestamp.UnixTimeStampToDateTime();

            // Assert
            epochStartDateTime.ShouldBeEquivalentTo(expectedDateTime);
            epochStartDateTime.Should().Be(expectedDateTime).And.BeIn(DateTimeKind.Utc);
        }

        [Fact]
        public void UnixTimeStampToDateTime_WithNegativeUnixTimestamp_ShouldReturnDateTimeBeforeStartOfUnixEpochAndShouldBeUtc()
        {
            // Arrange
            var expectedDateTime = new DateTime(1969, 12, 31, 23, 59, 50, 0, DateTimeKind.Utc);
            long unixTimestamp = -10;

            // Act
            DateTime epochStartDateTime = unixTimestamp.UnixTimeStampToDateTime();

            // Assert
            epochStartDateTime.ShouldBeEquivalentTo(expectedDateTime);
            epochStartDateTime.Should().Be(expectedDateTime).And.BeIn(DateTimeKind.Utc);
        }
    }
}
