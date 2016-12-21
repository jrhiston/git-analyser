using System;

namespace GitAnalyser.Interactor
{
    /// <summary>
    /// Represents a url for a repository.
    /// </summary>
    public class RepositoryUrl : IEquatable<RepositoryUrl>
    {
        private readonly string _url;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">The url to encapsulate.</param>
        public RepositoryUrl(string url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            _url = url;
        }

        /// <summary>
        /// Get the url this class encapsulates.
        /// </summary>
        public string Url => _url;

        public bool Equals(RepositoryUrl other) => other != null && other.Url == Url;
        public override string ToString() => Url;
        public override int GetHashCode() => _url.GetHashCode();
    }
}
