using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor.Commands
{
    public interface ICommandResult
    {
        ICommandVisitor Accept(ICommandVisitor visitor);
    }
}
