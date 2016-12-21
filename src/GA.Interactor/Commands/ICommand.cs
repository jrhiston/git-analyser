namespace GitAnalyser.Interactor.Commands
{
    internal interface ICommand<out TOutput>
    {
        TOutput Execute();
    }
}
