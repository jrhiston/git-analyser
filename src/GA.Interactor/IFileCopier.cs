namespace GitAnalyser.Interactor
{
    internal interface IFileCopier
    {
        void CopyGenerateGitLogFileToPath(
            string source,
            string destination,
            string file);
    }
}