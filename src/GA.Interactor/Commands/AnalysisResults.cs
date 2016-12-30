using GitAnalyser.Interactor.Pipes;
using System.Collections.Generic;
using System.Linq;

namespace GitAnalyser.Interactor.Commands
{
    public class AnalysisResults : 
        IEnumerable<ICommandResult>,
        ICommandResult
    {
        private readonly IEnumerable<ICommandResult> _elements;

        public AnalysisResults(params ICommandResult[] elements)
        {
            _elements = elements;
        }

        public IEnumerator<ICommandResult> GetEnumerator()
        {
            return this._elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public ICommandVisitor Accept(ICommandVisitor visitor)
        {
            return this._elements.Aggregate(visitor, (v, e) => e.Accept(v));
        }
    }
}
