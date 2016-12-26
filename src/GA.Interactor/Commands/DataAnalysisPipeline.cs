using GitAnalyser.Interactor.Pipes;
using System.Linq;
using System.IO;

namespace GitAnalyser.Interactor.Commands
{
    internal class DataAnalysisPipeline
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryUrl _repositoryUrl;
        private readonly RepositoryDestination _repositoryDestination;

        public IFileCopier FileCopier => _fileCopier;
        public RepositoryUrl RepositoryUrl => _repositoryUrl;
        public RepositoryDestination RepositoryDestination => _repositoryDestination;

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
                new ConditionalPipe<AnalysisResults>(
                    r => Directory.Exists(_repositoryDestination.ToString()), 
                    CreateFileDataReaderPipe(),
                    new CompositePipe<AnalysisResults>(
                        GenerateData().Concat(CreateFileDataReaderPipe()).ToArray())));

        private CompositePipe<AnalysisResults> CreateFileDataReaderPipe()
        {
            return new CompositePipe<AnalysisResults>(
                CreateFileDataReaderPipe("summary.csv", DataAnalysisResultType.Summary, _repositoryDestination),
                CreateFileDataReaderPipe("org-metrics.csv", DataAnalysisResultType.OrganisationMetrics, _repositoryDestination),
                CreateFileDataReaderPipe("coupling.csv", DataAnalysisResultType.Coupling, _repositoryDestination),
                CreateFileDataReaderPipe("age.csv", DataAnalysisResultType.Age, _repositoryDestination),
                CreateFileDataReaderPipe("abs-churn.csv", DataAnalysisResultType.AbsoluteChurn, _repositoryDestination),
                CreateFileDataReaderPipe("author-churn.csv", DataAnalysisResultType.AuthorChurn, _repositoryDestination),
                CreateFileDataReaderPipe("entity-churn.csv", DataAnalysisResultType.EntityChurn, _repositoryDestination),
                CreateFileDataReaderPipe("entity-ownership.csv", DataAnalysisResultType.EntityOwnership, _repositoryDestination),
                CreateFileDataReaderPipe("entity-effort.csv", DataAnalysisResultType.EntityEffort, _repositoryDestination));
        }

        private CommandVisitorPipe CreateFileDataReaderPipe(
            string fileToRead,
            DataAnalysisResultType type,
            RepositoryDestination destination) 
                => new CommandVisitorPipe(
                    new FileDataReaderCommand(fileToRead, type, destination));

        private CompositePipe<AnalysisResults> GenerateData() => 
            new CompositePipe<AnalysisResults>(
                new CommandVisitorPipe(
                    new CloneGitRepositoryCommand(_repositoryUrl, _repositoryDestination)),
                new CommandVisitorPipe(
                    new CopyFilesToDestinationCommand(FileCopier, _repositoryDestination)),
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, BenchmarkingFileNames.GitLogFileName)),
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, BenchmarkingFileNames.GitAnalysisFileName)));
    }
}
