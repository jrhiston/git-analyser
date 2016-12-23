using System;
using System.Collections;
using System.Collections.Generic;
using GitAnalyser.Interactor.Commands;

namespace GitAnalyser.Interactor.Pipes
{
    internal abstract class CommandResultVisitorBase : ICommandVisitor
    {
        public virtual ICommandVisitor Visit(CloneResult cloneResult) => this;
        public virtual ICommandVisitor Visit(CopyFilesResult copyFilesResult) => this;
        public virtual ICommandVisitor Visit(GeneratedDataResult generatedDataResult) => this;
        public virtual ICommandVisitor Visit(DataAnalysisResult dataAnalysisResult) => this;

        public virtual IEnumerator<ICommandResult> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
