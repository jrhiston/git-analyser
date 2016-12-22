using GitAnalyser.Interactor.Pipes;
using System;
using System.IO;
using System.Collections.Generic;

namespace GitAnalyser.Interactor.Commands
{
    internal class CopyFilesToDestinationCommand : CommandResultVisitorBase
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryDestination _repositoryDestination;

        public CopyFilesToDestinationCommand(
            IFileCopier fileCopier,
            RepositoryDestination repositoryDestination)
        {
            if (fileCopier == null)
                throw new ArgumentNullException(nameof(fileCopier));

            if (repositoryDestination == null)
                throw new ArgumentNullException(nameof(repositoryDestination));

            _fileCopier = fileCopier;
            _repositoryDestination = repositoryDestination;
        }

        public override IEnumerator<ICommandResult> GetEnumerator()
        {
            CopyBenchmarkingFilesToDestination(_repositoryDestination.ToString());

            yield return new CloneResult("success");
        }

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
    }
}
