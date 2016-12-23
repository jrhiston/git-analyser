using System.IO;

namespace GitAnalyser.Interactor
{
    internal class FileCopier : IFileCopier
    {
        public void CopyGenerateGitLogFileToPath(
            string source,
            string destination,
            string file)
        {
            var fileDestination = Path.Combine(destination, file);
            if (!File.Exists(fileDestination))
            {
                File.Copy(source, fileDestination);
            }
        }
    }
}
