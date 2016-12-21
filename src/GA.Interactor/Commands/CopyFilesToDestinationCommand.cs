using System;
using System.IO;

namespace GitAnalyser.Interactor.Commands
{
    internal class CopyFilesToDestinationCommand : ICommand<string>
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryDestination _destination;

        private CopyFilesToDestinationCommand(
            IFileCopier fileCopier,
            RepositoryDestination destination)
        {
            if (fileCopier == null)
                throw new ArgumentNullException(nameof(fileCopier));

            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            _fileCopier = fileCopier;
            _destination = destination;
        }

        public string Execute() => CopyBenchmarkingFilesToDestination(_destination.ToString());

        private string CopyBenchmarkingFilesToDestination(string destination)
        {
            CopyFileToDestination(destination, BenchmarkingFileNames.GitLogFileName);
            CopyFileToDestination(destination, BenchmarkingFileNames.GitAnalysisFileName);
            CopyFileToDestination(destination, BenchmarkingFileNames.CodeMaatFileName);

            return "success";
        }

        private void CopyFileToDestination(string destination, string fileName) =>
            _fileCopier.CopyGenerateGitLogFileToPath(
                Path.Combine(BenchmarkingFileNames.BenchmarkingFolderName, fileName),
                destination,
                fileName);

        public static CopyFilesToDestinationCommand Create(
            IFileCopier fileCopier,
            RepositoryDestination destination)
                => new CopyFilesToDestinationCommand(fileCopier, destination);
    }
}
