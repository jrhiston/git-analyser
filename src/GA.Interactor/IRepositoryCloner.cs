namespace GitAnalyser.Interactor
{
    public interface IRepositoryCloner
    {
        string Clone(string repositoryAddress, string folderName, string path = null);
    }
}