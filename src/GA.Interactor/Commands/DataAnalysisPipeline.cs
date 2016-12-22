using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor.Commands
{
    internal class DataAnalysisPipeline
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryUrl _repositoryUrl;
        private readonly RepositoryDestination _repositoryDestination;

        public DataAnalysisPipeline(
            IFileCopier fileCopier,
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            _fileCopier = fileCopier;
            _repositoryUrl = repositoryUrl;
            _repositoryDestination = repositoryDestination;
        }

        public CompositePipe<AnalysisResults> Create()
            => new CompositePipe<AnalysisResults>(
                new CommandVisitorPipe(
                    new CloneGitRepositoryCommand(_repositoryUrl, _repositoryDestination)),
                new CommandVisitorPipe(
                    new CopyFilesToDestinationCommand(_fileCopier, _repositoryDestination)),
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, BenchmarkingFileNames.GitLogFileName)),
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, BenchmarkingFileNames.GitAnalysisFileName)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "summary.csv",
                        DataAnalysisResultType.Summary,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "org-metrics.csv",
                        DataAnalysisResultType.OrganisationMetrics,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "coupling.csv",
                        DataAnalysisResultType.Coupling,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "age.csv",
                        DataAnalysisResultType.Age,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "abs-churn.csv",
                        DataAnalysisResultType.AbsoluteChurn,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "author-churn.csv",
                        DataAnalysisResultType.AuthorChurn,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "entity-churn.csv",
                        DataAnalysisResultType.EntityChurn,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "entity-ownership.csv",
                        DataAnalysisResultType.EntityOwnership,
                        _repositoryDestination)),
                new CommandVisitorPipe(
                    new FileDataReaderCommand(
                        "entity-effort.csv",
                        DataAnalysisResultType.EntityEffort,
                        _repositoryDestination)));
    }
}
