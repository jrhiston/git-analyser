using GitAnalyser.Interactor;
using System;
using Xunit;

namespace GA.Interactor.Tests
{
    public class RepositoryDestinationTests
    {
        [Fact]
        public void Constructor_ThrowArgumentNullException_GivenNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RepositoryDestination(null));
        }

        [Theory]
        [InlineData("Destination1")]
        [InlineData("Destination2")]
        [InlineData("Destination3")]
        public void Destination_EqualsGivenValue_GivenUrl(string dest)
        {
            var sut = new RepositoryDestination(dest);

            Assert.Equal(dest, sut.Destination);
        }

        [Fact]
        public void Equality_RepositoryDestinationsEqualEachOther_GivenTwoIdenticalDestinations()
        {
            var url = "url";
            var sut = new RepositoryDestination(url);
            var expected = new RepositoryDestination(url);

            Assert.Equal(expected, sut);
        }

        [Fact]
        public void ToString_ReturnsDestination_GivenDestination()
        {
            var url = "url";
            var sut = new RepositoryDestination(url);

            Assert.Equal(url, sut.ToString());
        }
    }
}
