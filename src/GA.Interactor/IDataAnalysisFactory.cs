using GitAnalyser.Interactor.Commands;

namespace GitAnalyser.Interactor
{
    internal interface IDataAnalysisFactory
    {
        ICommand<string> Create(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination);
    }
}