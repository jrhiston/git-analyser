namespace GitAnalyser.Interactor
{
    public interface IRepositoryCloner
    {
        IRepositoryCloner Clone(string repositoryAddress, string folderName, string path = null);

        string Result { get; set; }
    }
}