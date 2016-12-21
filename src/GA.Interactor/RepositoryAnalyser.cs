using System.IO;

namespace GitAnalyser.Interactor
{
    public class RepositoryAnalyser : IRepositoryAnalyser
    {
        private readonly IRepositoryCloner _repositoryCloner;
        private readonly IFileCopier _fileCopier;

        public RepositoryAnalyser(
            IRepositoryCloner repositoryCloner,
            IFileCopier fileCopier)
        {
            _repositoryCloner = repositoryCloner;
            _fileCopier = fileCopier;
        }

        public string Analyse(
            string destinationDirectory,
            string repositoryAddress,
            string folderName)
        {
            _repositoryCloner
                .Clone(destinationDirectory, repositoryAddress, folderName);

            string destination = Path.Combine(destinationDirectory, folderName);

            CopyBenchmarkingFilesToDestination(destination);

            ProcessRunner.RunCommand(destination, BenchmarkingFileNames.GitLogFileName);

            return ProcessRunner.RunCommand(
                destination,
                BenchmarkingFileNames.GitAnalysisFileName);
        }

        private void CopyBenchmarkingFilesToDestination(string destination)
        {
            _fileCopier.CopyGenerateGitLogFileToPath(
                Path.Combine(
                    BenchmarkingFileNames.BenchmarkingFolderName,
                    BenchmarkingFileNames.GitLogFileName),
                destination,
                BenchmarkingFileNames.GitLogFileName);

            _fileCopier.CopyGenerateGitLogFileToPath(
                Path.Combine(
                    BenchmarkingFileNames.BenchmarkingFolderName,
                    BenchmarkingFileNames.GitAnalysisFileName),
                destination,
                BenchmarkingFileNames.GitAnalysisFileName);
            _fileCopier.CopyGenerateGitLogFileToPath(
                Path.Combine(
                    BenchmarkingFileNames.BenchmarkingFolderName,
                    BenchmarkingFileNames.CodeMaatFileName),
                destination,
                BenchmarkingFileNames.CodeMaatFileName);
        }
    }
}
