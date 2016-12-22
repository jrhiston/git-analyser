using GitAnalyser.Interactor.Commands;
using System.Linq;

namespace GitAnalyser.Interactor.Pipes
{
    internal class CommandVisitorPipe : IPipe<AnalysisResults>
    {
        private readonly ICommandVisitor visitor;

        public CommandVisitorPipe(ICommandVisitor visitor)
        {
            this.visitor = visitor;
        }

        public AnalysisResults Pipe(AnalysisResults results)
        {
            var v = results.Accept(this.visitor);
            return new AnalysisResults(results.Concat(v).ToArray());
        }

        public ICommandVisitor Visitor
        {
            get { return this.visitor; }
        }
    }
}
