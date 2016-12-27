namespace GitAnalyser.Interactor.Pipes
{
    internal interface IPipeline<T>
    {
        CompositePipe<T> Create();
    }
}