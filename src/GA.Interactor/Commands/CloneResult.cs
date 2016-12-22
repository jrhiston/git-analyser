using System;
using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor.Commands
{
    public class CloneResult : ICommandResult
    {
        private readonly string _result;

        public CloneResult(string result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            _result = result;
        }

        public string Result => _result;

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
