using GitAnalyser.Interactor;
using GitAnalyser.Interactor.Commands;
using Moq;
using System;
using Xunit;

namespace GA.Interactor.Tests
{
    public class PipelineFactoryTests
    {
        [Fact]
        public void Should_ThrowArgumentNullException_When_NullFileCopierGiven()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new PipelineFactory(null));
        }

        [Fact]
        public void Should_CreateDataAnalysisPipeline_When_Called()
        {
            var sut = new PipelineFactory(Mock.Of<IFileCopier>());

            DataAnalysisPipeline result = (DataAnalysisPipeline) sut.CreateDataAnalysisPipeline(
                new RepositoryUrl("Url"),
                new RepositoryDestination("Destination"));

            Assert.NotNull(result);
        }

        [Fact]
        public void Should_PassFileCopierToPipeline_When_FileCopierGiven()
        {
            var fileCopierMock = Mock.Of<IFileCopier>();

            var sut = new PipelineFactory(fileCopierMock);

            DataAnalysisPipeline result = (DataAnalysisPipeline) sut.CreateDataAnalysisPipeline(
                new RepositoryUrl("Url"),
                new RepositoryDestination("Destination"));

            Assert.Same(fileCopierMock, result.FileCopier);
        }

        [Fact]
        public void Should_PassRepositoryUrlToPipeline_When_UrlGiven()
        {
            var url = new RepositoryUrl("url");
            var sut = new PipelineFactory(Mock.Of<IFileCopier>());

            DataAnalysisPipeline result = (DataAnalysisPipeline) sut.CreateDataAnalysisPipeline(
                url,
                new RepositoryDestination("Destination"));

            Assert.Same(url, result.RepositoryUrl);
        }

        [Fact]
        public void Should_PassRepositoryDestination_When_DestinationGiven()
        {
            var dest = new RepositoryDestination("destination");
            var sut = new PipelineFactory(Mock.Of<IFileCopier>());

            DataAnalysisPipeline result = (DataAnalysisPipeline) sut.CreateDataAnalysisPipeline(
                new RepositoryUrl("url"),
                dest);

            Assert.Same(dest, result.RepositoryDestination);
        }
    }
}
