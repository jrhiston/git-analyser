namespace GitAnalyser.Interactor.Pipes
{
    public interface IPipe<T>
    {
        T Pipe(T item);
    }
}
