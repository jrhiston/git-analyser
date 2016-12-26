using GitAnalyser.Interactor;
using GitAnalyser.Interactor.Commands;
using Moq;
using System;
using Xunit;

namespace GA.Interactor.Tests
{

    public class RepositoryAnalyserTests
    {
        [Fact]
        public void Should_ThrowArgumentNullException_When_FileCopierIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new RepositoryAnalyser(null, Mock.Of<IPipelineFactory>()));
        }

        [Fact]
        public void Should_ThrowArgumentNullException_When_PipelineFactoryIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new RepositoryAnalyser(Mock.Of<IFileCopier>(), null));
        }

        [Fact]
        public void Should_ExposePipelineFactory_When_GivenPipelineFactory()
        {
            var pipelineFactoryMock = Mock.Of<IPipelineFactory>();

            var sut = new RepositoryAnalyser(Mock.Of<IFileCopier>(), pipelineFactoryMock);

            Assert.Same(pipelineFactoryMock, sut.PipelineFactory);
        }

        [Fact]
        public void Should_ExposeFileCopier_When_GivenFileCopier()
        {
            var fileCopier = Mock.Of<IFileCopier>();

            var sut = new RepositoryAnalyser(fileCopier, Mock.Of<IPipelineFactory>());

            Assert.Same(fileCopier, sut.FileCopier);
        }

        [Fact]
        public void Should_CreateDataAnalysisPipelineWithParameters_When_GivenParameters()
        {
            var repositoryUrl = new RepositoryUrl("url");
            var repositoryDestination = new RepositoryDestination("destination");

            var expected = Mock.Of<DataAnalysisPipeline>();

            //var pipelineFactory = Mock.Of<IPipelineFactory>(
            //    pf => pf.CreateDataAnalysisPipeline(repositoryUrl, repositoryDestination) == )
        }
    }
}
