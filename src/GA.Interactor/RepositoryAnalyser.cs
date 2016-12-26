using GitAnalyser.Interactor.Commands;
using System;
using System.Threading.Tasks;

namespace GitAnalyser.Interactor
{
    internal class RepositoryAnalyser : IRepositoryAnalyser
    {
        private readonly IFileCopier _fileCopier;
        private readonly IPipelineFactory _pipelineFactory;

        public RepositoryAnalyser(
            IFileCopier fileCopier,
            IPipelineFactory pipelineFactory)
        {
            if (fileCopier == null)
                throw new ArgumentNullException(nameof(fileCopier));

            if (pipelineFactory == null)
                throw new ArgumentNullException(nameof(pipelineFactory));

            _fileCopier = fileCopier;
            _pipelineFactory = pipelineFactory;
        }

        public IPipelineFactory PipelineFactory => _pipelineFactory;
        public IFileCopier FileCopier => _fileCopier;

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
