using GitAnalyser.Interactor.Pipes;

namespace GitAnalyser.Interactor.Commands
{
    internal interface ICommand
    {
        ICommandVisitor Execute(ICommandVisitor visitor);
    }
}
