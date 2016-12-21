using System;

namespace GitAnalyser.Interactor.Commands
{
    internal class CloneGitRepositoryCommand : ICommand<string>
    {
        private readonly RepositoryUrl _repository;
        private readonly RepositoryDestination _destination;

        private CloneGitRepositoryCommand(
            RepositoryUrl repository,
            RepositoryDestination destination)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            if (destination == null)
                throw new ArgumentNullException(nameof(destination));


            _repository = repository;
            _destination = destination;
        }

        public RepositoryUrl Repository => _repository;
        public RepositoryDestination Destination => _destination;

        public string Execute()
        {
            return ProcessRunner.RunCommand(
                _destination.ToString(),
                "git.exe",
                $"clone {_repository.ToString()} {_destination.ToString()}");
        }

        public static CloneGitRepositoryCommand Create(
            RepositoryUrl repository,
            RepositoryDestination destination)
                => new CloneGitRepositoryCommand(repository, destination);
    }
}
