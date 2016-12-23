using GitAnalyser.Interactor.Pipes;
using System;

namespace GitAnalyser.Interactor.Commands
{
    public class CopyFilesResult : ICommandResult
    {
        private readonly string _result;

        public CopyFilesResult(string result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            _result = result;
        }

        public string Result => _result;

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
