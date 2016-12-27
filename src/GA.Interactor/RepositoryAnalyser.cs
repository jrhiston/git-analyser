using GitAnalyser.Interactor.Commands;
using System;
using System.Threading.Tasks;

namespace GitAnalyser.Interactor
{
    internal class RepositoryAnalyser : IRepositoryAnalyser
    {
        private readonly IPipelineFactory _pipelineFactory;

        public RepositoryAnalyser(IPipelineFactory pipelineFactory)
        {
            if (pipelineFactory == null)
                throw new ArgumentNullException(nameof(pipelineFactory));

            _pipelineFactory = pipelineFactory;
        }

        public IPipelineFactory PipelineFactory => _pipelineFactory;

        public AnalysisResults Analyse(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination) 
                => _pipelineFactory
                    .CreateDataAnalysisPipeline(repositoryUrl, repositoryDestination)
                    .Create()
                    .Pipe(new AnalysisResults());

        public Task<AnalysisResults> AnalyseAsync(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination) 
                => Task.Run(() => Analyse(repository, repositoryDestination));
    }
}
