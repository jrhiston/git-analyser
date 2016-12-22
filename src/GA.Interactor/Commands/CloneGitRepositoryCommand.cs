using GitAnalyser.Interactor.Pipes;
using System;
using System.Collections.Generic;

namespace GitAnalyser.Interactor.Commands
{
    internal class CloneGitRepositoryCommand : CommandResultVisitorBase
    {
        private readonly RepositoryUrl _repositoryUrl;
        private readonly RepositoryDestination _repositoryDestination;

        public CloneGitRepositoryCommand(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            if (repositoryUrl == null)
                throw new ArgumentNullException(nameof(repositoryUrl));

            if (repositoryDestination == null)
                throw new ArgumentNullException(nameof(repositoryDestination));

            _repositoryUrl = repositoryUrl;
            _repositoryDestination = repositoryDestination;
        }

        public override IEnumerator<ICommandResult> GetEnumerator()
        {
            yield return new CloneResult(ProcessRunner.RunGitCommand(
                $"clone {_repositoryUrl.ToString()} {_repositoryDestination.ToString()}"));
        }
    }
}
