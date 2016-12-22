using System;
using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor.Commands
{
    public class DataAnalysisResult : ICommandResult
    {
        private readonly string _result;
        private readonly DataAnalysisResultType _type;

        public DataAnalysisResult(string result, DataAnalysisResultType type)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            _result = result;
            _type = type;
        }

        public string Result => _result;
        public DataAnalysisResultType ResultType => _type;

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
