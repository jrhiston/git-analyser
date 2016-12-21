namespace GitAnalyser.Interactor
{
    public interface IRepositoryAnalyser
    {
        string Analyse(string destinationDirectory, string repositoryAddress, string folderName);
    }
}