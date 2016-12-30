using GitAnalyser.Interactor;
using GitAnalyser.Interactor.Commands;
using GitAnalyser.Interactor.Pipes;
using Moq;

namespace GA.Interactor.Tests.Builders
{
    internal class PipelineFactoryBuilder
    {
        private RepositoryUrl _repositoryUrl = new RepositoryUrl("URL");
        private RepositoryDestination _repositoryDestination = new RepositoryDestination("DEST");
        private IPipeline<AnalysisResults> _pipeline = Mock.Of<IPipeline<AnalysisResults>>(
            p => p.Create() == new CompositePipe<AnalysisResults>());

        public PipelineFactoryBuilder()
        {
            
        }

        public IPipeline<AnalysisResults> Pipeline => _pipeline;
        public RepositoryUrl RepositoryUrl => _repositoryUrl;
        public RepositoryDestination RepositoryDestination => _repositoryDestination;

        public PipelineFactoryBuilder SetRepositoryUrl(string repositoryUrl)
        {
            _repositoryUrl = new RepositoryUrl(repositoryUrl);

            return this;
        }

        public PipelineFactoryBuilder SetRepositoryDestination(string repositoryDestination)
        {
            _repositoryDestination = new RepositoryDestination(repositoryDestination);

            return this;
        }

        public PipelineFactoryBuilder SetPipeline(IPipeline<AnalysisResults> pipeline)
        {
            _pipeline = pipeline;

            return this;
        }

        public IPipelineFactory Build()
        {
            var pipeLineFactory = new Mock<IPipelineFactory>();

            pipeLineFactory
                .Setup(pf => pf.CreateDataAnalysisPipeline(_repositoryUrl, _repositoryDestination))
                .Returns(_pipeline);

            return pipeLineFactory.Object;
        }
    }
}