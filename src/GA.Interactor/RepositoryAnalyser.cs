using GitAnalyser.Interactor.Commands;
using System;
using System.Threading.Tasks;

namespace GitAnalyser.Interactor
{
    internal class RepositoryAnalyser : IRepositoryAnalyser
    {
        private readonly IFileCopier _fileCopier;

        public RepositoryAnalyser(IFileCopier fileCopier)
        {
            if (fileCopier == null)
                throw new ArgumentNullException(nameof(fileCopier));

            _fileCopier = fileCopier;
        }

        public AnalysisResults Analyse(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination) 
                => new DataAnalysisPipeline(_fileCopier, repository, repositoryDestination)
                    .Create()
                    .Pipe(new AnalysisResults());

        public Task<AnalysisResults> AnalyseAsync(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination) 
                => Task.Run(() => Analyse(repository, repositoryDestination));
    }
}
