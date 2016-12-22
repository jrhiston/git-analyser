using GitAnalyser.Interactor.Commands;
using System;
using System.IO;
using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor
{
    public class RepositoryDestination : IEquatable<RepositoryDestination>
    {
        private readonly string _destination;

        public RepositoryDestination(string destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            _destination = destination;
        }

        public string Destination => _destination;

        public bool Equals(RepositoryDestination other) 
            => other != null && string.Equals(_destination, other._destination);
        public override int GetHashCode() => _destination.GetHashCode();
        public override string ToString() => _destination;
    }
}
