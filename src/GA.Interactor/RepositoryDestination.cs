using System;
using System.IO;

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

        public RepositoryDestination(string containingFolder, string folderName) 
            : this(Path.Combine(containingFolder, folderName))
        {
        }

        public string Destination => _destination;

        public bool Equals(RepositoryDestination other) 
            => other != null && string.Equals(_destination, other._destination);
        public override int GetHashCode() => _destination.GetHashCode();
        public override string ToString() => _destination;
    }
}
