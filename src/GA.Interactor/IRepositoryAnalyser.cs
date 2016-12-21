namespace GitAnalyser.Interactor
{
    public interface IRepositoryAnalyser
    {
        string Analyse(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination);
    }
}