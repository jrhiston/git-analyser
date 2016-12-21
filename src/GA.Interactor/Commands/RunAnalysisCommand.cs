using System;

namespace GitAnalyser.Interactor.Commands
{
    internal class RunAnalysisCommand : ICommand<string>
    {
        private readonly RepositoryDestination _destination;

        private RunAnalysisCommand(RepositoryDestination destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            _destination = destination;
        }

        public string Execute()
        {
            string dest = _destination.ToString();

            return ProcessRunner.RunCommand(
                dest,
                BenchmarkingFileNames.GitAnalysisFileName);
        }

        public static RunAnalysisCommand Create(RepositoryDestination destination) 
            => new RunAnalysisCommand(destination);
    }
}
