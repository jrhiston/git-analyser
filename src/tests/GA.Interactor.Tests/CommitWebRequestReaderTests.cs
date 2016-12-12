using GitAnalyser.Interactor;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GA.Interactor.Tests
{
    [TestFixture]
    public class CommitWebRequestReaderTests
    {
        [Test]
        public async Task MethodRead_ReturnsCommits_WhenCalled()
        {
            var sut = new CommitWebRequestReader();

            var result = await sut.Read();

            Assert.That(result, Is.Not.Null);
        }
    }
}
