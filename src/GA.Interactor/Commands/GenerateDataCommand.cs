using System;
using System.Collections.Generic;
using GitAnalyser.Interactor.Pipes;
using System.Linq;

namespace GitAnalyser.Interactor.Commands
{
    internal class GenerateDataCommand : CommandResultVisitorBase
    {
        private readonly RepositoryDestination _repositoryDestination;
        private readonly string _fileNameToRun;

        public GenerateDataCommand(
            RepositoryDestination repositoryDestination,
            string fileNameToRun)
        {
            _repositoryDestination = repositoryDestination;
            _fileNameToRun = fileNameToRun;
        }

        public override IEnumerator<ICommandResult> GetEnumerator()
        {
            string dest = _repositoryDestination.ToString();

            var result = ProcessRunner.RunCommand(
                dest,
                _fileNameToRun);

            yield return new GeneratedDataResult(result);
        }

    }
}
