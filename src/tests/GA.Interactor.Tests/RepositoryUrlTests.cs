using GitAnalyser.Interactor;
using System;
using Xunit;

namespace GA.Interactor.Tests
{
    public class RepositoryUrlTests
    {
        [Fact]
        public void Constructor_ThrowArgumentNullException_GivenNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RepositoryUrl(null));
        }

        [Theory]
        [InlineData("Url1")]
        [InlineData("Url2")]
        [InlineData("Url3")]
        public void Url_EqualsGivenValue_GivenUrl(string url)
        {
            var sut = new RepositoryUrl(url);

            Assert.Equal(url, sut.Url);
        }

        [Fact]
        public void Equality_RepositoryUrlsEqualEachOther_GivenTwoIdenticalUrls()
        {
            var url = "url";
            var sut = new RepositoryUrl(url);
            var expected = new RepositoryUrl(url);

            Assert.Equal(expected, sut);
        }

        [Fact]
        public void ToString_ReturnsUrl_GivenUrl()
        {
            var url = "url";
            var sut = new RepositoryUrl(url);

            Assert.Equal(url, sut.ToString());
        }
    }
}
