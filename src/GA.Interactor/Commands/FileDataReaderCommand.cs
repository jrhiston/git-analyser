using GitAnalyser.Interactor.Pipes;
using System;
using System.Collections.Generic;
using System.IO;

namespace GitAnalyser.Interactor.Commands
{
    internal class FileDataReaderCommand : CommandResultVisitorBase
    {
        private readonly string _fileToRead;
        private readonly DataAnalysisResultType _type;
        private readonly RepositoryDestination _repositoryDestination;

        public FileDataReaderCommand(
            string fileToRead,
            DataAnalysisResultType type,
            RepositoryDestination repositoryDestination)
        {
            if (fileToRead == null)
                throw new ArgumentNullException(nameof(fileToRead));

            _fileToRead = fileToRead;
            _type = type;
            _repositoryDestination = repositoryDestination;
        }

        public override IEnumerator<ICommandResult> GetEnumerator()
        {
            yield return new DataAnalysisResult(
                File.ReadAllText(Path.Combine(_repositoryDestination.ToString(), _fileToRead)),
                _type);
        }
    }
}
