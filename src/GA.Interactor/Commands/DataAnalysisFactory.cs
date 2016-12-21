namespace GitAnalyser.Interactor.Commands
{
    internal class DataAnalysisFactory : IDataAnalysisFactory
    {
        private readonly IFileCopier _fileCopier;

        public DataAnalysisFactory(IFileCopier fileCopier)
        {
            _fileCopier = fileCopier;
        }

        public ICommand<string> Create(
            RepositoryUrl repositoryUrl, 
            RepositoryDestination repositoryDestination) 
                => new CompositeCommand<string>(
                    CloneGitRepositoryCommand.Create(repositoryUrl, repositoryDestination),
                    CopyFilesToDestinationCommand.Create(_fileCopier, repositoryDestination),
                    RunAnalysisCommand.Create(repositoryDestination));
    }
}
