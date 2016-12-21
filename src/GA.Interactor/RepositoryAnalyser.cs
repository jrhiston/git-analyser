using GitAnalyser.Interactor.Commands;
using System;
using System.IO;

namespace GitAnalyser.Interactor
{
    internal class RepositoryAnalyser : IRepositoryAnalyser
    {
        private readonly IFileCopier _fileCopier;
        private readonly IDataAnalysisFactory _factory;

        public RepositoryAnalyser(
            IFileCopier fileCopier,
            IDataAnalysisFactory dataAnalysisFactory)
        {
            if (fileCopier == null)
                throw new ArgumentNullException(nameof(fileCopier));

            if (dataAnalysisFactory == null)
                throw new ArgumentNullException(nameof(dataAnalysisFactory));

            _fileCopier = fileCopier;
            _factory = dataAnalysisFactory;
        }

        public string Analyse(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination)
        {
            var result = _factory
                .Create(repository, repositoryDestination)
                .Execute();

            return result;
        }
    }
}
