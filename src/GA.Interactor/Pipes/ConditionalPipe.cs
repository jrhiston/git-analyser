using System;

namespace GitAnalyser.Interactor.Pipes
{
    public class ConditionalPipe<T> : IPipe<T>
    {
        public ConditionalPipe(Func<T, bool> condition, IPipe<T> success, IPipe<T> failure)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            if (success == null)
                throw new ArgumentNullException(nameof(success));

            if (failure == null)
                throw new ArgumentNullException(nameof(failure));

            Condition = condition;
            Success = success;
            Failure = failure;
        }

        public Func<T, bool> Condition { get; }
        public IPipe<T> Success { get; }
        public IPipe<T> Failure { get; }

        public T Pipe(T item) => Condition(item) ? Success.Pipe(item) : Failure.Pipe(item);
    }
}
